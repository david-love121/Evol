using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet
{
    public class SingleLayerPerceptron : Perceptron
    {
        public SingleLayerPerceptron(int nInputs, int nOutputs)
        {
            for (int i = 0; i < nOutputs; ++i)
            {
                OutputNodes.Add(new Node());
            }

            for (int i = 0; i < nInputs; ++i)
            {
                var node = new Node();
                foreach (var output in OutputNodes)
                {
                    node.Connectors.Add(new Connector() { Weight = 1, Node = output });
                }
                InputNodes.Add(node);
            }
        }
        public override void Run()
        {
            InputNodes.ForEach((x) => x.FeedForward());
        }

        public override Perceptron Clone()
        {
            var clone = new SingleLayerPerceptron(InputNodes.Count, OutputNodes.Count);

            for (int inode = 0; inode < InputNodes.Count; ++inode)
            {
                for (int iconnect = 0; iconnect < InputNodes[inode].Connectors.Count; ++iconnect)
                {
                    clone.InputNodes[inode].Connectors[iconnect].Weight =
                        InputNodes[inode].Connectors[iconnect].Weight;
                }
            }

            return clone;
        }

        protected override IEnumerable<Connector> Connectors
        {
            get
            {
                foreach (var node in InputNodes)
                {
                    foreach (var connector in node.Connectors)
                    {
                        yield return connector;
                    }
                }
            }
        }

        protected override void PrivateWrite(BinaryWriter bw)
        {
            bw.Write("SingleLayer");
            var nodeDict = new Dictionary<Node, int>();
            int counter = 0;

            // Input nodes
            bw.Write(InputNodes.Count);
            foreach (var node in InputNodes)
            {
                nodeDict.Add(node, counter++);
            }

            // Output nodes
            bw.Write(OutputNodes.Count);
            foreach (var node in OutputNodes)
            {
                nodeDict.Add(node, counter++);
            }

            // Connectors
            bw.Write(Connectors.Count());
            foreach (var node in InputNodes)
            {
                foreach (var connector in node.Connectors)
                {
                    bw.Write(nodeDict[node]);
                    bw.Write(nodeDict[connector.Node]);
                    bw.Write(connector.Weight);
                }
            }
        }
    }
}