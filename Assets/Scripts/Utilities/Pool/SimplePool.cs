/// Simple pooling for Unity.
///   Author: Martin "quill18" Glaude (quill18@quill18.com). Modified by Michael OReilly
///   Latest Version: https://gist.github.com/quill18/5a7cfffae68892621267
///   License: CC0 (http://creativecommons.org/publicdomain/zero/1.0/)
///   UPDATES:
/// 	2015-04-16: Changed Pool to use a Stack generic.
/// 
/// Usage:
/// 
///   There's no need to do any special setup of any kind.
/// 
///   Instead of calling Instantiate(), use this:
///       SimplePool.Spawn(somePrefab, somePosition, someRotation);
/// 
///   Instead of destroying an object, use this:
///       SimplePool.Despawn(myGameObject);
/// 
///   If desired, you can preload the pool with a number of instances:
///       SimplePool.Preload(somePrefab, 20);
/// 
/// Remember that Awake and Start will only ever be called on the first instantiation
/// and that member variables won't be reset automatically.  You should reset your
/// object yourself after calling Spawn().  (i.e. You'll have to do things like set
/// the object's HPs to max, reset animation states, etc...)
/// 
/// 
/// 

using UnityEngine;
using System.Collections.Generic;

namespace Asteroids.Utilities.Pools
{
    public static class SimplePool
    {
        // You can avoid resizing of the Stack's internal data by
        // setting this to a number equal to or greater to what you
        // expect most of your pool sizes to be.
        // Note, you can also use Preload() to set the initial size
        // of a pool -- this can be handy if only some of your pools
        // are going to be exceptionally large (for example, your bullets.)
        private const int DEFAULT_POOL_SIZE = 3;

        // All of our pools
        private static Dictionary<PoolMember, Pool> pools;

        /// <summary>
        /// Initialize our dictionary.
        /// </summary>
        static void Init(PoolMember prefab = null, int qty = DEFAULT_POOL_SIZE)
        {
            if (pools == null)
            {
                pools = new Dictionary<PoolMember, Pool>();
            }
            if (prefab != null && !pools.ContainsKey(prefab))
            {
                pools.Add(prefab, new Pool(prefab, qty));
            }
        }

        /// <summary>
        /// If you want to preload a few copies of an object at the start
        /// of a scene, you can use this. Really not needed unless you're
        /// going to go from zero instances to 100+ very quickly.
        /// Could technically be optimized more, but in practice the
        /// Spawn/Despawn sequence is going to be pretty darn quick and
        /// this avoids code duplication.
        /// </summary>
        static public void Preload(PoolMember prefab, int qty = 1)
        {
            Init(prefab, qty);

            // Make an array to grab the objects we're about to pre-spawn.
            PoolMember[] obs = new PoolMember[qty];
            for (int i = 0; i < qty; i++)
            {
                obs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
            }

            // Now despawn them all.
            for (int i = 0; i < qty; i++)
            {
                Despawn(obs[i]);
            }
        }

        /// <summary>
        /// Spawns a copy of the specified prefab (instantiating one if required).
        /// NOTE: Remember that Awake() or Start() will only run on the very first
        /// spawn and that member variables won't get reset.  OnEnable will run
        /// after spawning -- but remember that toggling IsActive will also
        /// call that function.
        /// </summary>
        static public PoolMember Spawn(PoolMember prefab, Vector3 pos, Quaternion rot)
        {
            Init(prefab);
            return pools[prefab].Spawn(pos, rot);
        }

        static public List<PoolMember> GetActiveInstances(PoolMember prefab)
        {
            return pools[prefab].GetActiveInstances();
        }

        /// <summary>
        /// Despawn the specified gameobject back into its pool.
        /// </summary>
        static public void Despawn(PoolMember poolMember)
        {
            if (poolMember.Pool == null)
            {
                Debug.Log("Object '" + poolMember.name + "' wasn't spawned from a pool. Destroying it instead.");
                Object.Destroy(poolMember);
            }
            else
            {
                poolMember.Pool.Despawn(poolMember);
            }
        }

    }
}