using AspNetCoreAngularSignalR.SignalRHubs;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreAngularSignalR.Providers
{
    public class NewsStore
    {
        private List<string> _newsGroups = new List<string>();
        private List<NewsItem> _newsItems = new List<NewsItem>();

        public void AddGroup(string group)
        {
            _newsGroups.Add(group);
        }

        public bool GroupExists(string group)
        {
            return _newsGroups.Contains(group);
        }

        public void CreateNewItem(NewsItem item)
        {
            if (GroupExists(item.NewsGroup))
            {
                _newsItems.Add(item);
            }

            throw new System.Exception("group does not exist");
        }

        public IEnumerable<NewsItem> GetAllNewsItems(string group)
        {
            return _newsItems.Where(item => item.NewsGroup == group);
        }

        public List<string> GetAllGroups()
        {
            return _newsGroups;
        }
    }
}
