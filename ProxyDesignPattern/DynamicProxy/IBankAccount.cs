﻿namespace ProxyDesignPattern.DynamicProxy
{
    public interface IBankAccount
    {
        void Deposit(int amount);
        bool Withdraw(int amount);
        string ToString();
    }
}