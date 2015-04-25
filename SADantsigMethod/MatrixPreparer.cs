using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADantsigMethod
{
    class MatrixPreparer//класс отвечающий за перевичную подготовку массива к выполнению поиска максимума
    {

        private List<List<double>> coefficArray = new List<List<double>>();//локальный массив коэффициентов

        /// <summary>
        /// Конструктор подготовителя массива
        /// </summary>
        /// <param name="targetArray">Подгоотавливаемая матрица</param>
        public MatrixPreparer(List<List<double>> targetArray)
        {
            this.coefficArray = targetArray;//сохранить массив в поле coefArray для работы с ним
        }



        /// <summary>
        /// Поиск и удаление нулевых переменных из массива чисел
        /// </summary>
        private void zeroColumsSlice()
        {
            for (int i = 0; i < coefficArray[0].Count; i++)//перебор столбцов (без последнего)
            {
                double absSummColum = 0;//сумма коэффициентов в колоне по модулю
                foreach (List<double> coefLine in coefficArray)//перебор всех строк
                {
                    absSummColum += Math.Abs(coefLine[i]);//подсчет абсолютной суммы всех коэффициентов в столюце
                }
                if (absSummColum == 0)//если сумма = 0
                {
                    removeColum(i);//удаление колонки
                }
            }
        }

        /// <summary>
        /// Удаление колонки коэффициентов
        /// </summary>
        /// <param name="index"></param>
        private void removeColum(int index)
        {
            foreach (List<double> coefLine in coefficArray)//перебор всех строк
            {
                coefLine.RemoveAt(index);
            }
        }

        /// <summary>
        /// Поиск и удаление нулевого ограничения
        /// </summary>
        private void zeroStringSlice()
        {
            for (int index = 1; index < coefficArray.Count; index++)//перебор всех строк 
            {
                double absSummString = 0;//абсолютная сумма коэффициентов
                foreach (double coeffic in coefficArray[index])//перебор всех колонок, кроме последней
                {
                    absSummString += Math.Abs(coeffic);//подсчет абсолютной суммы коэффициентов в строке
                }
                if (absSummString == 0)//если сумма = 0 
                {
                    coefficArray.RemoveAt(index);//удалить строку
                }
            }
        }

        /// <summary>
        /// Уничтожение нулевых строк и столбцов матрицы
        /// </summary>
        private void zeroTest()
        {
            zeroColumsSlice();//удаление нулевых столбцов
            zeroStringSlice();//удаление нулевых строк
        }

        /// <summary>
        /// Приведение матрицы к каноничному виду
        /// </summary>
        private void toCanonForm()
        {
            int addIndex = 1;//индекс ограничения, к которому будет добавлен не нулевой коэффициент
            while (coefficArray.Count-1 >= coefficArray[0].Count)//до тех пор пока ограничений больше переменных вводить новые переменные
            {
                for (int i = 0; i < coefficArray.Count; i++)
                {
                    if (i == addIndex)
                    {
                        coefficArray[i].Add(1);
                    }
                    else
                    {
                        coefficArray[i].Add(0);
                    }
                }
                addIndex++;
            }
        }

        /// <summary>
        /// Приведение матрицы или её части к виду едиеничной матрицы методом Гаусса 
        /// </summary>
        public void gaussMethod()
        {
            for (int i = 1; i < coefficArray.Count; i++)///обнуление нижнего угла матрицы
            {
                if (coefficArray[i][i - 1] == 0)//установка не нудевых элементов на главную диагональ
                {
                    int index = findStrIndex(i);
                    List<double> temp = coefficArray[i];
                    coefficArray[i] = coefficArray[index];
                    coefficArray[index] = temp;
                }
                coefficArray[i] = multStr(coefficArray[i], 1 / coefficArray[i][i - 1]);//приравнивание первого эл строки к 1
                for (int j = i + 1; j < coefficArray.Count; j++)//отчистка нижнего угла матрицы
                {
                    coefficArray[j] = subStrs(coefficArray[j], multStr(coefficArray[i], coefficArray[j][i - 1]));
                }
            }
            for (int i = coefficArray.Count - 1; i > 1; i--)//отчистка верхнего угла матрицы
            {
                for (int j = i - 1; j > 0; j--)
                {
                    coefficArray[j] = subStrs(coefficArray[j], multStr(coefficArray[i], coefficArray[j][i - 1]));
                }
            }
        }

        /// <summary>
        /// Умножение всех элементов строки на число
        /// </summary>
        /// <param name="str">Строка</param>
        /// <param name="multiplier">Число</param>
        /// <returns></returns>
        private List<double> multStr(List<double> str, double multiplier)
        {
            List<double> temp = str.GetRange(0, str.Count);

            for (int i = 0; i < temp.Count; i++)
            {
                temp[i] *= multiplier;//умножение элемента на число
            }
            return temp;//вернуть полученную строку
        }

        /// <summary>
        /// Вычитание строк
        /// </summary>
        /// <param name="arg1">Строка-вычитаемое</param>
        /// <param name="arg2">Строка-вычитатель</param>
        /// <returns></returns>
        private List<double> subStrs(List<double> arg1, List<double> arg2)
        {
            List<double> temp = arg1.GetRange(0, arg1.Count);
            for (int i = 0; i < arg1.Count; i++)
            {
                temp[i] -= arg2[i];//вычесть соответствующие элементы строк
            }
            return temp;//вернуть полученную строку
        }


        /// <summary>
        /// Поиск индекса строки с не нулевым элементом первого столбца
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private int findStrIndex(int currentIndex)
        {
            for (int i = currentIndex; i < coefficArray.Count; i++)
            {
                if (coefficArray[i][currentIndex - 1] != 0)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Выполнить подготовку матрицы к выполнению операции
        /// </summary>
        /// <returns></returns>
        public List<List<double>> prepareMatrix()
        {
            zeroTest();//Удалить нулевые строки и столбцы
            toCanonForm();//Привести к каноничному виду
            gaussMethod();
            return this.coefficArray;
        }

        /// <summary>
        /// Вернуть матрицу, хранимую в данном объекте
        /// </summary>
        /// <returns></returns>
        public List<List<double>> getMatrix()
        {
            return this.coefficArray;
        }

    }
}
