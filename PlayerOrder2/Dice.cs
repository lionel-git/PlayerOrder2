using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerOrder2
{
    public class Dice
    {
        private static int _faces;
        private string _name;
        private static Random _rand = new Random(42);

        double[] _probability;

        public static void SetFaces(int faces)
        {
            _faces = faces;
        }

        public Dice(string name)
        {
            _name = name;
            _probability = new double[_faces];
            
            double sum = 0;
            for (int i = 0; i < _faces; i++)
            {
                _probability[i] = _rand.NextDouble();
                sum += _probability[i];
            }
            for (int i = 0; i < _faces; i++)
                _probability[i] /= sum;
        }

        public double ProbaVictory(Dice B)
        {
            double proba = 0.0;
            for (int i = 0; i < _faces; i++)
            {
                for (int j = i+1; j < _faces; j++)
                {
                    // On a j > i
                    // This tire j et B tire i => victoire
                    // This tire i et B tire j => defaite
                    // Si i = j => nulle
                    proba += _probability[j] * B._probability[i] - _probability[i] * B._probability[j];
                }
            }
            return proba;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{_name}: ");
            for (int i = 0; i < _faces; i++)
                sb.Append($"{_probability[i]:0.00} ");
            return sb.ToString();
        }

    }
}
