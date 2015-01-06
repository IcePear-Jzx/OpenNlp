﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNLP.Tools.Util
{
    /**
 * Type signature for a class that supports the basic operations required
 * of a typesafe heterogeneous map.
 *
 * @author dramage
 */

    public interface TypesafeMap
    {

        /**
   * Returns true if the map contains the given key.
   * todo [cdm 2014]: This is synonymous with containsKey(), but used less, so we should just eliminate it.
   */
        bool has(Type key);

        /**
   * Returns the value associated with the given key or null if
   * none is provided.
   */
        object get(Type key);

        /**
   * Associates the given value with the given type for future calls
   * to get.  Returns the value removed or null if no value was present.
   */
        object set(Type key, object value);

        /**
   * Removes the given key from the map, returning the value removed.
   */
        object remove(Type key);

        /**
   * Collection of keys currently held in this map.  Some implementations may
   * have the returned set be immutable.
   */
        Set<Type> keySet();
        //public Set<Class<? extends Key<?>>> keySet();

        /**
   * Returns true if contains the given key.
   */
        bool containsKey(Type key);

        /**
   * Returns the number of keys in the map.
   */
        int size();
    }

    /**
   * Base type of keys for the map.  The classes that implement Key are
   * the keys themselves - not instances of those classes.
   *
   * @param <VALUE> The type of the value associated with this key.
   */

    public interface Key<T>
    {
    }
}