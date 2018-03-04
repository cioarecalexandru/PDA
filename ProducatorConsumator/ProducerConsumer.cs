using System.Collections;
using System.Threading;

namespace ProducatorConsumator
{
    public class ProducerConsumer
    {
        static Queue queue = new Queue();

        public void Produce(object o)
        {
           
            lock (queue)
            {
                queue.Enqueue(o);
                Monitor.Pulse(queue);
            }
        }

        public object Consume()
        {
            lock (queue)
            {
                while(queue.Count == 0)
                {
                    Monitor.Wait(queue);
                }
                return queue.Dequeue();
            }
        }
    }
}