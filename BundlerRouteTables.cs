﻿using System.Collections.Generic;
using System.Web.Management;
using System.Web.Routing;

namespace BundlerMiddleware
{
    using System.Collections.Concurrent;
    using System.Linq;

    public class BundlerRoutes
    {
        public static BundlerRouteTable Routes = new BundlerRouteTable();
    }

    public class BundlerRouteTable : ICollection<BundlerRoute>
    {
        private readonly IDictionary<string, BundlerRoute> Routes = new ConcurrentDictionary<string, BundlerRoute>();

        public bool Exists(string route)
        {
            return Routes.ContainsKey(route);
        }

        public BundlerRoute Get(string route)
        {
            return Routes[route];
        }

        public void Add(BundlerRoute item)
        {
            Routes.Add(item.Route, item);
        }

        public void Clear()
        {
            Routes.Clear();
        }

        public bool Contains(BundlerRoute item)
        {
            return Routes[item.Route] != null;
        }

        public void CopyTo(BundlerRoute[] array, int arrayIndex)
        {
            Routes.Select(r => r.Value).ToList().CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Routes.Count; }
        }

        public bool IsReadOnly
        {
            get { return Routes.IsReadOnly;  }
        }

        public bool Remove(BundlerRoute item)
        {
            return Routes.Remove(item.Route);
        }

        public IEnumerator<BundlerRoute> GetEnumerator()
        {
            return Routes.Select(r => r.Value).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Routes.GetEnumerator();
        }
    }

    public class BundlerRoute
    {
        public string Route { get; set; }
        public string FilePath { get; set; }

        public BundlerRoute(string route, string filePath)
        {
            this.Route = route;
            this.FilePath = filePath;
        }

        public static BundlerRoute BundlerRouteFromVirutalPath(string route, string virutalPath)
        {
            return new BundlerRoute(route, System.Web.VirtualPathUtility.ToAbsolute(virutalPath));
        }
    }
}
