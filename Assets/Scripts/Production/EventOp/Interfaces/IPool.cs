﻿public interface IPool<T>
{
    T Rent(bool returnActive);
}
