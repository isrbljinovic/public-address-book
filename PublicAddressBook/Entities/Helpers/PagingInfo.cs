namespace Entities.Helpers
{
    public class PagingInfo
    {
        private int _pageSize = 2;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value < 2)
                    _pageSize = 2;
                else if (value > 50)
                    _pageSize = 50;
                else
                    _pageSize = value;
            }
        }

        private int _pageNumber = 1;

        public int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                if (value < 1)
                    _pageNumber = 1;
                else
                    _pageNumber = value;
            }
        }
    }
}