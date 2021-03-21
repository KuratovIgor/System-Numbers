using System;
using System.Collections;
using System.Collections.Generic;

namespace LibSystemNumbers
{
    public static class Number
    {
        private static String _number; //Введённое пользователем число
        private static String _intNumber; //Целая часть числа
        private static String _floatNumber; //Дробная часть числа
        private static int _basis; //Система счисления, в котором находится введённое число
        private static int _basis2; //Система счисления, в которую нужно перевести введённое число
        private const int MinBase = 2; //Минимальное значение системы счисления
        private const int MaxBase = 36; //Максимальное значение системы счисления

        //Функция перевода символьного значения числа в целочисленное
        private static int CharToInt(char symbol) { 
            //symbol - Символьное значения числа
            if (symbol >= '0' && symbol <= '9') return symbol - '0';
            else {
                if (symbol >= 'A' && symbol <= 'Z') return symbol - 'A' + 10;
                else return 100000;
            }
        }
        //Функция перевода целочисленного значения числа в символьное
        private static char IntToChar(int numbers) {
            //numbers - целочисленное значение числа
            if (numbers <= 9) return (char)(numbers + '0');
            else return (char)(numbers + 55);
        }
        
        //Функция проверки коррекности введённого числа
        private static bool IsNumberCorrect() {
            int length = _number.Length; //Длина числа
            int countPoint = 0; //Количество точек в числе

            if (_number[0] == '.' || _number[length - 1] == '.') return false;

            for (int i = 0; i < length; i++) {
                if (countPoint > 1) return false;
                if (_number[i] == '.') {
                    countPoint++;
                    continue;
                }
                if (CharToInt(_number[i]) >= _basis) return false;
            }

            return true;
        }

        //Функция проверки корректности введенных систем счисления.
       //Система счисления не может быть меньше 2 и больше 36
        private static bool IsBasisCorrect() {
            if ((_basis > MaxBase || _basis < MinBase) || (_basis2 > MaxBase || _basis2 < MinBase)) {
                return false;
            }
            return true;
        }

        //Функция перевода целой части числа в десятичную систему счисления
        private static int TranslateIntegerToDecimal() {
            int decimalNumber = 0, //Хранит число в десятичной системе счисления
                index = 1; //Хранит степень, в которую нужно возводить каждую цифру числа
            int count = _intNumber.Length; //Хранит длину целочисленной части числа

            for (int i = count - 1; i >= 0; i--) {
                int bufNumber = CharToInt(_intNumber[i]);
                decimalNumber += (bufNumber * index);
                index *= _basis;
            }

            return decimalNumber;
        }

        //Функция перевода дробной части числа в десятичную систему счисления
        private static double TranslateFloatToDecimal() {
            double floatNumber = 0.0; //Хранит число в десятичной системе счисления
            double index = -1; //Хранит степень, в которую нужно возводить каждую цифру числа
            int count = _floatNumber.Length; //Хранит длину целочисленной части числа
            double bufBasis = _basis; //Хранит систему счисления, в которую нужно перевести число.
                                     //Создана для того, чтобы не изменять значение переменной _basis

            for (int i = 0; i < count; i++) {
                double bufNumber = CharToInt(_floatNumber[i]);
                floatNumber = floatNumber + (bufNumber * Math.Pow(bufBasis, index));
                index--;
            }

            return floatNumber;
        }

        //Функция перевода буквы в число и числа в букву в том случае, если значение числа записано в 
        //системе счисления, большей 9
        private static char TranslateToMoreSystems(char number)
        {
            //Словарь латинского алфавита
            Dictionary<char, int> dictionaryOfNumbers = new Dictionary<char, int>(26) {
                {'A', 10}, {'B', 11}, {'C', 12}, {'D', 13}, {'E', 14}, {'F', 15}, {'G', 16}, {'H', 17}, {'I', 18},
                {'J', 19}, {'K', 20}, {'L', 21}, {'M', 22}, {'N', 23}, {'O', 24}, {'P', 25}, {'Q', 26}, {'R', 27},
                {'S', 28}, {'T', 29}, {'U', 30}, {'V', 31}, {'W', 32}, {'X', 33}, {'Y', 34}, {'Z', 35}
            };
            //Словарь чисел
            Dictionary<int, char> dictionaryOfSymbols = new Dictionary<int, char>(26) {
                {10, 'A'}, {11, 'B'}, {12, 'C'}, {13, 'D'}, {14, 'E'}, {15, 'F'}, {16, 'G'}, {17, 'H'}, {18, 'I'},
                {19, 'J'}, {20, 'K'}, {21, 'L'}, {22, 'M'}, {23, 'N'}, {24, 'O'}, {25, 'P'}, {26, 'Q'}, {27, 'R'},
                {28, 'S'}, {29, 'T'}, {30, 'U'}, {31, 'V'}, {32, 'W'}, {33, 'X'}, {34, 'Y'}, {35, 'Z'}
            };

            bool isCheckSymbol = false; //Проверяет, является ли введённое значение буквой
            if (number >= 'A' && number <= 'Z')
            {
                isCheckSymbol = true;
            }
            else
            {
                isCheckSymbol = false;
            }

            if (isCheckSymbol == false) return dictionaryOfSymbols[CharToInt(number)];
            else return IntToChar(dictionaryOfNumbers[number]);
        }

        //Функция перевода дробной части числа в систему счисления _basis2
        private static char[] TranslateFractionalPart()
        {
            int digit = 0, //Хранит цифры из дробной части
                index = 0,
                size = 50;
            char[] arrNumbers = new char[size]; //Массив цифр из дробной части в системе счисления _basis2
            double floatNumbers = TranslateFloatToDecimal(); //Перевод дробной части в десятичную систему счисления

            int sizeFloatNumbers = 0; //Хранит длину дробной части в десятичной системе счисления
            double bufNumber = floatNumbers;
            //Вычисление размера дробной части в десятичной системе счисления
            while (bufNumber % 10 != 0)
            {
                bufNumber *= 10;
                sizeFloatNumbers++;
            }

            sizeFloatNumbers--;

            //Перевод дробной части в систему счисления _basis2
                 //Дробная часть при переводе в любую счисления может быть очень большой, поэтому
                //дробную часть помещаем в 10-ти разрядную сетку
                while (index < 10)
                {
                    floatNumbers *= _basis2;
                    digit = (int) floatNumbers;
                    arrNumbers[index] = IntToChar(digit);
                    floatNumbers -= digit;
                    index++;
                }

                arrNumbers[index] = '\0';

            return arrNumbers;
        }

        //Функция перевод числа в систему счисления _basis2
        public static char[] TranslateToSystemNumbers(string stringNumbers, int firstBasis, int secondBasis) {
            //stringNumbers - введённое число 
            //firstBasis - система счисления, в которой записано введённое число
            //secondBasis - система счисления, в которую нужно перевести число
            _number = stringNumbers;
            _basis = firstBasis;
            _basis2 = secondBasis;
            char[] nullArrNumbers = { '\0' }; //Хранит пустой массив символов. Возвращается в случае
                                             //некорректности записи числа либо несоответсвии числа с системой счисления 

            if (IsBasisCorrect() == false) {
                Console.WriteLine("Basis must be not less than 2 and not more than 36!");
                return nullArrNumbers;
            }
            if (IsNumberCorrect() == false) { //Проверка на корректность записи введённого числа
                Console.WriteLine("Number isn't correct!");
                return nullArrNumbers;
            }
            else {
                int j = 0;
                bool checkPoint = false; //Проверяет, есть ли в числе дробная часть (точка)

                //Разбиваем число на целую и дробную части
                while (j != stringNumbers.Length) { 

                    if (stringNumbers[j] == '.') {
                        checkPoint = true;
                        j++;
                        continue;
                    }
                    else {
                        if (checkPoint == false) 
                            _intNumber += stringNumbers[j];
                        else {
                            _floatNumber += stringNumbers[j];
                        }

                        j++;
                    }
                }

                int count = 0; //Хранит количество цифр в записи числа в системе счисления _basis2
                int decNumbers = TranslateIntegerToDecimal(); //Записываем число в десятичной системе счисления
                int bufDecNumbers = decNumbers; //Переменная-буфер для хранения копии десятичной записи числа,
                                                //чтобы не изменять значение decNumbers

                //Вычисление количества цифр в записи числа в системе счисления _basis2
                while (bufDecNumbers > 0) {
                    bufDecNumbers /= _basis2;
                    count++;
                }

                char[] arrNumbers = new char[count]; //Массив цифр в системе счисления _basis2
                char[] resultString = new char[100]; //Результат в виде числа в системе счисления _basis2
                int index = 0; 

                //Перевод числа в систему счисления _basis2
                while (decNumbers > 0) {
                    arrNumbers[index] = IntToChar(decNumbers % secondBasis);
                    if (CharToInt(arrNumbers[index]) > 9) arrNumbers[index] = TranslateToMoreSystems(arrNumbers[index]);
                    decNumbers /= _basis2;
                    index++;
                }

                index = 0;
                //Запись числа в системе счисления _basis2 в корректный вид
                for (int i = 0; i < count; i++) {
                    resultString[i] = arrNumbers[count - 1 - i];
                    index++;
                }

                if (checkPoint == true) {
                    char[] floatNumbers = new char[50]; //Массив цифр из дробной части в системе счисления _basis2
                    int sizeFloatNumbers = 0; //Хранит размер дробной части
                    floatNumbers = TranslateFractionalPart(); //Перевод дробной части в систему счисления _basis2

                    resultString[index] = '.';

                    //Вычисления размера дробной части
                    while (floatNumbers[sizeFloatNumbers] != '\0') sizeFloatNumbers++;

                    int ind = 0; 
                    //Присоединение дробной части в системе счисления _basis2 к результату из целой части
                    for (int i = index + 1; i < sizeFloatNumbers + index + 1; i++) {
                        resultString[i] = floatNumbers[ind];
                        ind++;
                    }
                }

                return resultString;
            }
        }
    }
}

