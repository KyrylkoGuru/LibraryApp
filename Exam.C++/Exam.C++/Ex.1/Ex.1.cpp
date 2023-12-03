#include <iostream>


const int SIZE = 6; // Розмір масиву

double PositiveDobutok(double a[])
{
    double dobutok = 1.0;

    for (int i = 0; i < SIZE; ++i)
    {
        if (a[i] > 0)
        {
            dobutok *= a[i];
        }
    }

    return dobutok;
}

// Функція сортування парних елементів за зростанням
void SortParni(double a[], int size)
{
    for (int i = 0; i < size; i += 2)
    {
        for (int j = i + 2; j < size; j += 2)
        {
            if (a[i] > a[j])
            {
                double temp = a[i];
                a[i] = a[j];
                a[j] = temp;
            }
        }
    }
}

// Функція сортування непарних елементів за зростанням
void SortNeparni(double a[], int size)
{
    for (int i = 1; i < size; i += 2)
    {
        for (int j = i + 2; j < size; j += 2)
        {
            if (a[i] > a[j])
            {
                double temp = a[i];
                a[i] = a[j];
                a[j] = temp;
            }
        }
    }
}

int main()
{
    double a[SIZE];

    std::cout << "Enter the elements of the array:" << std::endl;
    for (int i = 0; i < SIZE; ++i)
    {
        std::cout << "a[" << i << "]: ";
        std::cin >> a[i];
    }

    double PD = PositiveDobutok(a);
    std::cout << "Product of positive elements: " << PD << std::endl;

    double min = a[0];
    for (int i = 1; i < SIZE; ++i)
    {
        if (a[i] < min)
        {
            min = a[i];
        }
    }

    double sumBeforeMin = 0.0;
    for (int i = 0; i < SIZE && a[i] != min; ++i)
    {
        sumBeforeMin += a[i];
    }
    std::cout << "The sum of the elements to the minimum element is: " << sumBeforeMin << std::endl;

    // Сортування парних та непарних елементів за зростанням
    SortParni(a, SIZE);
    SortNeparni(a, SIZE);

    std::cout << "Even elements: ";
    for (int i = 0; i < SIZE; i += 2)
    {
        std::cout << a[i] << " ";
    }
    std::cout << std::endl;

    std::cout << "Odd elements: ";
    for (int i = 1; i < SIZE; i += 2)
    {
        std::cout << a[i] << " ";
    }
    std::cout << std::endl;

    return 0;
}
