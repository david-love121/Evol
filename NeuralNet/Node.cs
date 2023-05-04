using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet
{
    public class Node
    {
        public List<Connector> Connectors { get; set; } = new List<Connector>();

        private double total = 0;

        public void Reset()
        {
            total = 0;
        }

        public void AddData(double value, double weight = 1)
        {
            total += value * weight;
        }

        public void FeedForward()
        {
            Connectors.ForEach((x) => x.Node.AddData(total, x.Weight));
        }

        public double GetValue()
        {
            return total;
        }
    }
}
