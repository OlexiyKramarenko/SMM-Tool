﻿namespace SMMSender.Utils.Optional
{
    public sealed class None<T> : Option<T>, IEquatable<None<T>>, IEquatable<None>
    {
        public override Option<TResult> Map<TResult>(Func<T, TResult> map) =>
            None.Value;

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map) =>
            None.Value;

        public override T Reduce(T whenNone) =>
            whenNone;

        public override T Reduce(Func<T> whenNone) =>
            whenNone();

        public override Option<TNew> OfType<TNew>() => new None<TNew>();

        public override bool Equals(object obj) =>
            !(obj is null) && ((obj is None<T>) || (obj is None));

        public override int GetHashCode() => 0;

        public bool Equals(None<T> other) =>
            other?.GetType() == typeof(None<T>);

        public bool Equals(None other) => true;

        public static bool operator ==(None<T> a, None<T> b) =>
            a?.Equals(b) ?? b is null;

        public static bool operator !=(None<T> a, None<T> b) => !(a == b);

        public override string ToString() => "None";
    }

    public sealed class None : IEquatable<None>
    {
        public static None Value { get; } = new None();

        private None() { }

        public override string ToString() => "None";

        public override bool Equals(object obj) =>
            !(obj is null) && ((obj is None) || this.IsGenericNone(obj.GetType()));

        private bool IsGenericNone(Type type) =>
            type.GenericTypeArguments.Length == 1 &&
            typeof(None<>).MakeGenericType(type.GenericTypeArguments[0]) == type;

        public bool Equals(None other) => true;

        public override int GetHashCode() => 0;
    }
}
