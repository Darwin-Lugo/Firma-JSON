﻿namespace Firma.Core.CustomEntities
{
    public class MetaData
    {
        #region Property
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string NexPageUrl { get; set; } = null!;
        public string PreviousPageUrl { get; set; } = null!;
        #endregion
    }
}
