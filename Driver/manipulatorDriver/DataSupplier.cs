﻿using System.Collections.Generic;

namespace manipulatorDriver
{

    // Todo: Considerable usage 
    public abstract class DataSupplier
    {
        private List<Observer> observers;

        public DataSupplier()
        {
            observers = new List<Observer>();

        }

        public void Subscribe(Observer observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(Observer observer)
        {
            observers.Remove(observer);

        }

        public void NotifyObservers(string data)
        {
            foreach(var observer in observers)
             {
                observer.getNotified(data);
            }
        }
    }
}
