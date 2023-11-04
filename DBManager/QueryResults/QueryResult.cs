namespace DBManager.QueryResults
{
    public class QueryResult : IQueryResult
    {
        private bool result = false;
        public QueryResult()
        { }

        public QueryResult(bool r)
        {
            result = r;
        }

        public bool GetResult()
        { return result; }
    }
}
