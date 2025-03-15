﻿using System.Collections;

namespace Academy.SharedKernel
{
    public class ErrorList : IEnumerable<Error>
    {
        private readonly List<Error> _errors;

        public ErrorList(IEnumerable<Error> errors)
        {
            _errors = [..errors];
        }

        public IEnumerator<Error> GetEnumerator() => _errors.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static implicit operator ErrorList(Error error) => new([error]);
    }
}
