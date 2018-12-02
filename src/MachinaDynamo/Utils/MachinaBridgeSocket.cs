﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSocketSharp;

namespace MachinaDynamo
{
    /// <summary>
    /// A quick wrapper class that incorporates a log of received messages...
    /// </summary>
    internal class MachinaBridgeSocket
    {
        public WebSocket socket;
        public string name;
        public List<string> receivedMessages = new List<string>();
        private object bufferLock = new object();

        public MachinaBridgeSocket(string socketName)
        {
            this.name = socketName;
        }

        public void Log(string msg)
        {
            receivedMessages.Add(msg);
        }

        public void Flush()
        {
            receivedMessages.Clear();
        }

        public int BufferSize() => receivedMessages.Count;

        public string FetchFirst(bool remove)
        {
            lock (bufferLock)
            {
                if (receivedMessages.Count == 0)
                {
                    return null;
                }

                string first = receivedMessages[0];

                if (remove)
                {
                    receivedMessages.RemoveAt(0);
                }

                return first;
            }
        }

        public List<string> FetchBuffer(bool remove)
        {
            lock (bufferLock)
            {
                if (receivedMessages.Count == 0)
                {
                    return new List<string>();
                }
                
                List<string> clone = new List<string>(receivedMessages);

                if (remove)
                {
                    Flush();
                }

                return clone;
            }
        }
    }
}
