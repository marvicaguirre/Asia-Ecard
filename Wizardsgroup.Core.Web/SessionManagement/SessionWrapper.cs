using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace Wizardsgroup.Core.Web.SessionManagement
{
    public class SessionWrapper : IHttpSessionState 
    {
        private readonly HttpSessionState _session = HttpContext.Current.Session;

        public void Abandon()
        {
            _session.Abandon();
        }

        public void Add(string name, object value)
        {
            _session.Add(name, value);
        }

        public void Remove(string name)
        {
            _session.Remove(name);
        }

        public void RemoveAt(int index)
        {
            _session.RemoveAt(index);
        }

        public void Clear()
        {
            _session.Clear();
        }

        public void RemoveAll()
        {
            _session.RemoveAll();
        }

        public IEnumerator GetEnumerator()
        {
            return _session.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            _session.CopyTo(array, index);
        }

        public string SessionID
        {
            get
            {
                return _session.SessionID;
            }
        }

        public int Timeout
        {
            get { return _session.Timeout; }
            set { _session.Timeout = value; }
        }

        public bool IsNewSession
        {
            get { return _session.IsNewSession; }
        }

        public SessionStateMode Mode
        {
            get { return _session.Mode; }
        }

        public bool IsCookieless
        {
            get { return _session.IsCookieless; }
        }

        public HttpCookieMode CookieMode
        {
            get { return _session.CookieMode; }
        }

        public int LCID
        {
            get { return _session.LCID; }
            set { _session.LCID = value; }
        }

        public int CodePage
        {
            get { return _session.CodePage; }
            set { _session.CodePage = value; }
        }

        public HttpStaticObjectsCollection StaticObjects
        {
            get { return _session.StaticObjects; }
        }

        object IHttpSessionState.this[string name]
        {
            get { return _session[name]; }
            set { _session[name] = value; }
        }

        object IHttpSessionState.this[int index]
        {
            get { return _session[index]; }
            set { _session[index] = value; }
        }

        public int Count
        {
            get { return _session.Count; }
        }

        public NameObjectCollectionBase.KeysCollection Keys
        {
            get { return _session.Keys; }
        }

        public object SyncRoot
        {
            get { return _session.SyncRoot; }
        }

        public bool IsReadOnly
        {
            get { return _session.IsReadOnly; }
        }

        public bool IsSynchronized
        {
            get { return _session.IsSynchronized; }
        }

        public object this[string name]
        {
            get { return _session[name]; }
            set { _session[name] = value; }
        }

        public object this[int index]
        {
            get { return _session[index]; }
            set { _session[index] = value; }
        }
    }
}