using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Neural_network
{
    public class NeuroNet
    {
        public Neuron[] neurons = new Neuron[10];
        public int rows, columns;

        public NeuroNet(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            for (int i = 0; i < 10; i++)
            {
                string[] fileNames = Directory.GetFiles("D:\\World\\универ\\разин\\Интеллектуальные системы\\Интеллектуальные системы\\Neural_network\\Neural_network\\Numbers\\" + i);
                if (fileNames.Length > 0)
                {
                    string fileName = fileNames[0];
                    double[,] tmpNeuronWeights = load(fileName);
                    neurons[i] = new Neuron(tmpNeuronWeights, rows, columns);
                }
            }
        }
        public void learn()
        {
            string[] fileNames = Directory.GetFiles("D:\\World\\универ\\разин\\Интеллектуальные системы\\Интеллектуальные системы\\Neural_network\\Neural_network\\Numbers\\", "*", SearchOption.AllDirectories);
            for (int i = 0; i < fileNames.Length; i++)
            {
                double[,] currentWeights = load(fileNames[i]);
                double maxFitness = 0;
                Neuron bestNeuron = neurons[0];
                for (int j = 0; j < 10; j++)
                {
                    double currentFitness = neurons[j].fitness(currentWeights);
                    if (currentFitness > maxFitness)
                    {
                        maxFitness = currentFitness;
                        bestNeuron = neurons[j];
                    }
                }
                bestNeuron.addWeights(currentWeights);
            }
        }
        public int recognize(double[,] weights)
        {
            int result = 0;
            double maxFitness = 0;
            for (int j = 0; j < 10; j++)
            {
                double currentFitness = neurons[j].fitness(weights);
                if (currentFitness > maxFitness)
                {
                    maxFitness = currentFitness;
                    result = j;
                }
            }
            return result;
        }
        private double[,] load(string fileName)
        {
            double[,] localMatrix = new double[rows, columns];

            StreamReader fs = new StreamReader(fileName);
            string line;
            string[] values;
            int i = 0;
            while (!(fs.EndOfStream))
            {
                line = fs.ReadLine();
                values = line.Split(' ');
                for (int j = 0; j < values.Length; j++)
                {
                    if (values[j] == "1")
                        localMatrix[i, j] = 1;
                    else
                        localMatrix[i, j] = 0;
                }
                i++;
            }
            fs.Close();
            return localMatrix;
        }
    }
}
