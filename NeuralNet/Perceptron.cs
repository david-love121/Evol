using DongUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNet
{
    abstract public class Perceptron
    {
        static public Random Random { get; } = new();

        int NInputs => InputNodes.Count;
        int NOutputs => OutputNodes.Count;

        protected List<Node> InputNodes { get; } = new();
        protected List<Node> OutputNodes { get; } = new();

        protected abstract IEnumerable<Connector> Connectors { get; }

        public void AddInput(int index, double value)
        {
            InputNodes[index].AddData(value);
        }

        public void AddInputs(params double[] values)
        {
            if (values.Length != NInputs)
            {
                throw new ArgumentException("Wrong number of arguments passed to AddInputs()!");
            }
            for (int i = 0; i < values.Length; ++i)
            {
                AddInput(i, values[i]);
            }
        }

        public void Reset()
        {
            InputNodes.ForEach((x) => x.Reset());
            OutputNodes.ForEach((x) => x.Reset());
        }

        abstract public void Run();

        abstract public Perceptron Clone();

        public Perceptron RandomClone(double standardDeviation)
        {
            var clone = Clone();

            clone.RandomWeights(standardDeviation);

            return clone;
        }

        public double GetOutput(int index)
        {
            return OutputNodes[index].GetValue();
        }

        public IEnumerable<double> GetOutputs()
        {
            foreach (var node in OutputNodes)
            {
                yield return node.GetValue();
            }
        }

        public void RandomWeights(double standardDeviation)
        {
            foreach (var connector in Connectors)
            {
                connector.Weight *= Random.NextGaussian(0, standardDeviation);
            }
        }

        public void WriteToFile(string filename)
        {
            using var bw = new BinaryWriter(File.Create(filename));
            PrivateWrite(bw);
        }

        abstract protected void PrivateWrite(BinaryWriter bw);

        static public Perceptron ReadFromFile(string filename)
        {
            using var br = new BinaryReader(File.OpenRead(filename));

            var type = br.ReadString();
            if (type == "SingleLayer")
            {
                int nInputNodes = br.ReadInt32();
                int nOutputNodes = br.ReadInt32();

                var perceptron = new SingleLayerPerceptron(nInputNodes, nOutputNodes);
                var nodeDict = new Dictionary<int, Node>();
                for (int i = 0; i < nInputNodes; ++i)
                {
                    var newNode = new Node();
                    perceptron.InputNodes.Add(newNode);
                    nodeDict.Add(i, newNode);
                }
                for (int i = nInputNodes; i < nInputNodes + nOutputNodes; ++i)
                {
                    var newNode = new Node();
                    perceptron.OutputNodes.Add(newNode);
                    nodeDict.Add(i, newNode);
                }

                int nConnectors = br.ReadInt32();
                for (int i = 0; i < nConnectors; ++i)
                {
                    int inputNode = br.ReadInt32();
                    var connector = new Connector();
                    int outputNode = br.ReadInt32();
                    connector.Node = nodeDict[outputNode];
                    connector.Weight = br.ReadDouble();

                    nodeDict[inputNode].Connectors.Add(connector);
                }
                return perceptron;
            }

            throw new FormatException("Invalid perceptron format");
        }
    }
}
