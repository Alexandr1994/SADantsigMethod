using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADantsigMethod
{
    class DantsigCalculator
    {

        private List<List<double>> coefficArray = new List<List<double>>();//входной массив коэффициентов ограничений и функции для вычислений
        private List<double> resValues = new List<double>();//массив базисных решений
        private List<List<double>> localCoefArray = new List<List<double>>();//массив коэффициентов
        private int[] basisVarsIndexes;//массив индексов базисных переменных
        private int godRowIndex;//индекс "неприкосновенной" строки при смене базиса
        private int allowElIndex;//индкс разрешающего элемента в строке

        /// <summary>
        /// Установка входных данных вычислительного класса
        /// </summary>
        /// <param name="data">Массив коэффициентов</param>
        /// <param name="resArray">Массив Базисных Решений</param>
        public DantsigCalculator(List<List<double>> data, List<double> resArray)
        {
            coefficArray = data;
            resValues = resArray;
        }


        /// <summary>
        /// Поиск стартовых индексов базисных переменных(после преобразования гаусса)
        /// </summary>
        /// <returns></returns>
        private void chooseStartBasis()
        {
            basisVarsIndexes = new int[coefficArray.Count - 1];
            for (int i = 0; i < basisVarsIndexes.Length; i++)
            {
                for (int j = i; j < coefficArray[i + 1].Count; j++)
                {
                    if (columnTest(j))
                    {
                        basisVarsIndexes[i] = j;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Проверка является ли переменная с индексом varIndex базисной
        /// </summary>
        /// <param name="varIndex"></param>
        /// <returns></returns>
        private bool columnTest(int varIndex)
        {
            double coefSum = 0;//сумма индексов данной переменной
            for (int i = 1; i < coefficArray.Count; i++)//пробежаться по матрице коэффициентов 
            {
                coefSum += Math.Abs(coefficArray[i][varIndex]);//и найти абсолютную сумму коеффициентов переменной
            }
            if (coefSum.Equals(1))//Если сумма коеффициенов равна 1
            {
                return true;//то данная переменная является базисной
            }
            return false;//иначе не является
        }

        /// <summary>
        /// Сформировать локальный массив коэффициентов 
        /// </summary>
        private void formLocalCoefArray()
        {
            List<double> coefLine;
            for (int i = 0; i < basisVarsIndexes.Length; i++)
            {
                coefLine = new List<double>();
                foreach (double coef in coefficArray[findRestrictIndex(basisVarsIndexes[i])])
                {
                    coefLine.Add(coef);
                }
                localCoefArray.Add(coefLine);
            }
            localCoefArray.Add(findZ(localCoefArray));
            localCoefArray.Add(findDelta(localCoefArray));
        }

        /// <summary>
        /// Поиск ограничения, где коэффициент данной базисной переменной равен 1
        /// </summary>
        /// <param name="basisIndex"></param>
        /// <returns></returns>
        private int findRestrictIndex(int basisIndex)
        {
            for (int i = 1; i < coefficArray.Count; i++)//пройтись по строкам ограничений массива
            {
                if (coefficArray[i][basisVarsIndexes[basisIndex]] == 1)//и если элемент строки с указанным индексом = 1
                {
                    return i;//вернуть индекс данной строки
                }
            }
            return -1;//иначе вернуть -1
        }

        /// <summary>
        /// Найти относительные оценки Z
        /// </summary>
        /// <returns></returns>
        private List<double> findZ(List<List<double>> matrix)
        {
            List<double> ZLine = new List<double>();
            for (int i = 0; i < matrix[0].Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < matrix.Count; j++)
                {
                    sum += coefficArray[0][basisVarsIndexes[j]] * matrix[j][i];
                }
                ZLine.Add(sum);
            }
            return ZLine;
        }

        /// <summary>
        /// Найти относительные оценки Дельта
        /// </summary>
        /// <returns></returns>
        private List<double> findDelta(List<List<double>> matrix)
        {
            int indexZ = matrix.Count - 1;
            List<double> DeltaLine = new List<double>();
            for (int i = 0; i < matrix[0].Count; i++)
            {
                DeltaLine.Add(coefficArray[0][i] - matrix[indexZ][i]);
            }
            return DeltaLine;
        }

        /// <summary>
        /// Проверка на выполнение условия окончания поиска максимума
        /// </summary>
        /// <returns></returns>
        private bool canFinish()
        {
            int deltaIndex = localCoefArray.Count - 1;
            foreach (double value in localCoefArray[deltaIndex])//перебрать элементы строки Дельта
            {
                if (value > 0)//если в строке Дельта есть положительные числа
                {
                    return true;//то необходимо продолжить вычисления
                }
            }
            return false;//иначе все оценки отрицательны или равны 0 => процесс вычислений можно завершить
        }

        /// <summary>
        /// Найти новый набор базисных переменных
        /// </summary>
        private void findNewBasis()
        {
            allowElIndex = findMaxDeltaIndex();//индекс новой базисной переменной = индексу максимальной "Дельты"
            godRowIndex = findChangeVarIndex(allowElIndex);//найти и сохранить индекс неизменяющейся строки
            basisVarsIndexes[godRowIndex] = allowElIndex;//замена базисной переменной в массиве
        }

        /// <summary>
        /// Найти столбец с наибольшим приращением Дельта
        /// </summary>
        /// <returns></returns>
        private int findMaxDeltaIndex()
        {
            List<double> deltaLine = localCoefArray[localCoefArray.Count - 1];//строка "Дельт" - последняя строка массива
            return deltaLine.IndexOf(deltaLine.Max());//вернуть индекс максимального значения строки
        }

        /// <summary>
        /// Найти индекс заменяемой переменной базиса
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private int findChangeVarIndex(int columnIndex)
        {
            List<double> divArray = findDivs(columnIndex);//найти массив частных
            return divArray.IndexOf(divArray.Min());//вернуть индекс наименьшего элемента массива, который соответствует индексу заменяемой переменной
        }

        /// <summary>
        /// Найти массив частных базисных решений на элементы указанного столбца 
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private List<double> findDivs(int columnIndex)
        {
            List<double> divArray = new List<double>();//инициализация массива
            for (int i = 0; i < resValues.Count; i++)//
            {
                if (localCoefArray[i][columnIndex] > 0)//если элемент выбранного столбца массива коеффициентов больше 0
                {
                    divArray.Add(resValues[i] / localCoefArray[i][columnIndex]);//разделить текущее базисное решение на этот элемент
                }
                else
                {
                    divArray.Add(Double.MaxValue);//иначе исключить данный элемент из рассмотрения
                }
            }
            return divArray;//вернуть найденный массив
        }

        /// <summary>
        /// Копирование значений строки матрицы в новую строку, с возможностью деление каждого элемента данной строки на число
        /// </summary>
        /// <param name="targetStr">Целевая строка</param>
        /// <param name="diver">Делитель</param>
        /// <returns></returns>
        private List<double> copyString(List<double> targetStr, double diver)
        {
            List<double> retStr = new List<double>();//возвращаемая строка
            foreach (double value in targetStr)
            {
                retStr.Add(value / diver);//поэлементное копирование
            }
            return retStr;//вернуть копию строки
        }

        /// <summary>
        /// Построение новой симплекс таблицы
        /// </summary>
        private void constructNewMatrix()
        {
            List<List<double>> newCoefMatrix = new List<List<double>>();//создание временной матрицы
            List<double> newRes = new List<double>();//массив базисных ренений
            for (int i = 0; i < resValues.Count; i++)
            {
                newCoefMatrix.Add(new List<double>());//инициализация строки времменой матрицы
                if (i == godRowIndex)//если рассматриваемая строка разрешающая, 
                {
                    newCoefMatrix[i] = copyString(localCoefArray[i], localCoefArray[godRowIndex][allowElIndex]);//то сохранить её в временной матрице, разделенную на разрешающий элемент
                    newRes.Add(resValues[i]);//сохранить старое базисное решение
                    continue;
                }
                for (int j = 0; j < localCoefArray[i].Count; j++)//иначе пересчитать каждые элемент строки
                {
                    newCoefMatrix[i].Add(localCoefArray[i][j] - (localCoefArray[i][allowElIndex] * localCoefArray[godRowIndex][j]) / localCoefArray[godRowIndex][allowElIndex]);
                }
                newRes.Add(resValues[i] - (resValues[godRowIndex] * localCoefArray[i][allowElIndex]) / localCoefArray[godRowIndex][allowElIndex]);//вычисление нового базисного решения
            }
            newCoefMatrix.Add(findZ(newCoefMatrix));//вычисление новогл массива Z
            newCoefMatrix.Add(findDelta(newCoefMatrix));//вычисление нового массива Delta
            localCoefArray.Clear();//отчистить устаревшую матрицу
            resValues.Clear();//отчистить устаревшие базисные решения
            localCoefArray = newCoefMatrix;//сохранение вычисленной матрице в массиве
            resValues = newRes;//сохранение вычисленных базисных решений


        }

        /// <summary>
        /// Получить найденный максимум
        /// </summary>
        /// <returns></returns>
        public double getMax()
        {
            double sum = 0;//сумма
            for (int i = 0; i < basisVarsIndexes.Length; i++)//вычисление максимума
            {
                sum = resValues[i] * coefficArray[0][basisVarsIndexes[i]];
            }
            return sum;
        }

        /// <summary>
        /// Получение вектора значений переменных
        /// </summary>
        /// <returns></returns>
        public double[] getVarsValuesVector()
        {
            double[] retVector = new double[coefficArray[0].Count];//инициализация масс
            for (int i = 0; i < retVector.Length; i++)//заполнение возвращаемого вектора
            {
                retVector[i] = 0;//установк очередного элемента вектора в 0
                for (int j = 0; j < basisVarsIndexes.Length; j++)//проверка переменной на принадлежность к базису
                {

                    if (i == basisVarsIndexes[j])//если переменная относится к базису
                    {
                        retVector[i] = resValues[j];//внести в элемент вектора соответствующее базисное решение
                        break;
                    }
                }
            }
            return retVector;//вернуть найденный вектор
        }


        /// <summary>
        /// Основная вычислительная функция, возвращает true в случае успешного выполнения вычислений
        /// </summary>
        /// <returns></returns>
        public bool calculationProcess()
        {
            int counter = 0;
            chooseStartBasis();
            formLocalCoefArray();
            while (canFinish())
            {
                findNewBasis();
                constructNewMatrix();
                counter++;
                if (counter > 10000000)
                {
                    return false;
                }
            }
            return true;
        }









    }
}
