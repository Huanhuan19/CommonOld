using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCYDisLabServer
{
    public class ClientManager
    {
        public ClientManager()
        {
            _clients = new List<ClientInfo>();
        }
        #region Props
        List<ClientInfo> _clients;
        public List<ClientInfo> Clients
        {
            get { return _clients; }
        }
        #endregion

        #region Methods

        public void Add(byte[] values)
        {
            if (!Contains(values))
            {
                _clients.Add(new ClientInfo(values));
            }
            else
            {
                Update(values);
            }
        }
        public void Remove(int index )
        {
            if (index >= 0 && index < _clients.Count)
            {
                _clients.RemoveAt(index);
            }
        }
        public int Get(byte[] values)
        {
            int index = -1;
            for (int i = 0; i < _clients.Count; i++)
            {
                if (Match(i, values))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public bool Match(int index, byte[] values)
        {
            bool match = false;
            if (index >= 0 && index < _clients.Count)
            {
                ClientInfo client = _clients[index];
                ClientInfo newClient = new ClientInfo(values);
                if (client.Match(newClient))
                {
                    match = true;
                }
            }
            return match;
        }
        public bool Contains(byte[] values)
        {
            bool contains = false;
            for (int i = 0; i < _clients.Count; i++)
            {
                if (Match(i, values))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
        public void Update(byte[] values)
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                if (Match(i, values))
                {
                    _clients[i].Initialize(values);
                    break;
                }
            }
        }
        #endregion
    }
}
