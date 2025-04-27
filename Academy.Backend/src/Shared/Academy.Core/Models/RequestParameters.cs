﻿namespace Academy.Core.Models
{
    public abstract class RequestParameters
    {
        private const int _maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value <= _maxPageSize ? value : _maxPageSize; }
        }
    }
}
