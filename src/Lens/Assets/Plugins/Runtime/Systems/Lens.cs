using System;

namespace AmarilisLib
{
    /// <summary>
    /// Represents a lens that provides access and modification to a part of a data structure.
    /// </summary>
    /// <typeparam name="TTargetValue">The type of the whole data structure.</typeparam>
    /// <typeparam name="TTargetData">The type of the part of the data structure.</typeparam>
    public class Lens<TTargetValue, TTargetData>
    {
        /// <summary>
        /// A function to get the part of the data structure from the whole.
        /// </summary>
        public Func<TTargetValue, TTargetData> Get { get; }

        /// <summary>
        /// A function to set the part of the data structure and return the updated whole.
        /// </summary>
        public Func<TTargetValue, TTargetData, TTargetValue> Set { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lens{S, T}"/> class with the specified getter and setter functions.
        /// </summary>
        /// <param name="getter">A function to get the part of the data.</param>
        /// <param name="setter">A function to set the part of the data and return the modified whole.</param>
        public Lens(Func<TTargetValue, TTargetData> getter, Func<TTargetValue, TTargetData, TTargetValue> setter)
        {
            Get = getter;
            Set = setter;
        }

        /// <summary>
        /// Composes this lens with another lens, creating a new lens that operates on a deeper part of the data.
        /// </summary>
        /// <typeparam name="TNewTargetValue">The type of the new target part of the data.</typeparam>
        /// <param name="other">The lens to compose with.</param>
        /// <returns>A new lens that operates on the data of type <typeparamref name="TNewTargetValue"/>.</returns>
        public Lens<TTargetValue, TNewTargetValue> Compose<TNewTargetValue>(Lens<TTargetData, TNewTargetValue> other)
        {
            return new Lens<TTargetValue, TNewTargetValue>(
                s => other.Get(Get(s)),
                (s, v) => Set(s, other.Set(Get(s), v))
            );
        }
    }
}
