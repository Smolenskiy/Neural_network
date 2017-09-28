using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_network
{
    public class Neuron
    {
        public double[,] weights;
        public double fullWeight = 0;
        public int rows, columns;

        public Neuron() { }
        public Neuron(double[,] weights,int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.weights = new double[rows,columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if(weights[i,j]>0)
                    {
                        addNeighbourWeights(j,i,1);
                    }
                }
            }
        }

        public double fitness(double[,] weights)
        {
            double result=0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    result += weights[i, j] * this.weights[i, j];
            }
            return result/fullWeight;
        }

        public void addWeights(double[,] weights)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if(weights[i,j]>0)
                    {
                        addNeighbourWeights(j,i,1);
                    }
                }
            }
        }

        public void addNeighbourWeights(int x, int y, double k)
        {
            for (int k1=-1;k1<2;k1++)
            {
                for (int k2=-1; k2<2; k2++) 
                {
                    if (y+k1 < 0 || y+k1>=rows || x+k2 < 0 || x+k2>=columns) 
                    {
                        continue;
                    }
                    fullWeight +=1/ (Math.Abs(k1)+Math.Abs(k2)+k);
                    weights[y+k1,x+k2] += 1 / (Math.Abs(k1)+Math.Abs(k2)+k);
                }
            }
        }
    }
}
