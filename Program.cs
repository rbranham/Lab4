﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Web;
/*
 * Lab 4 - Roger Branham,  
 * In collaboration with Jessie Souders
 */

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            XYZ company = new XYZ(); //Create Company

            //Create list of desks
            List<Desk> l = new List<Desk>();
            l.Add(new Desk(20.30, 1, "A"));
            l.Add(new Desk(15.25, 2, "B"));
            l.Add(new Desk(25.13, 3, "C"));
            l.Add(new Desk(5.85, 4, "D"));


            company.setList(l); //Set list to company for test
            company.askUtility();//Sets up utility
            company.mainSortingTest();// Run the prescibed sort from the promt
            
        }
    }

    class XYZ
    {

        private Utility<Desk> u;
        private List<Desk> l;

        public void setList(List<Desk> l) { this.l = l; }

        public void askUtility()
        {
            u = new ProxyUtility<Desk>("Quick Sort"); //specifices the quick sort algorithm
        }

        public void mainSortingTest()
        {
            if (l != null) { u.sort(l); } //Run sorting alorithm. 
        }
    }

    abstract class Utility<T> where T : IComparable<T>, ProductIF {
        private String sortName;


        public Utility()
        {
            sortName = "Bubble Sort"; //Set algorithm to bubble sort
        }

        /**
         * Creates a utility class initialized to a different algo
         */
        public Utility(String sortName)
        {
            this.sortName = sortName;
        }

        abstract public List<T> sort(List<T> data);


        /**
         * Getter method for sortName attribute
         */
        public String getSortName() { return sortName; }
        
    }

    class ProxyUtility<T> : Utility<T> where T : IComparable<T>, ProductIF
    {
        private Utility<T> u; 

        public ProxyUtility()
        {
            u = new BSUtility<T>(); //Delegate to the bubble sort Utility
        }
        public ProxyUtility(String sortName) : base(sortName) {
            if (this.getSortName() == "Bubble Sort") //Check for Bubble Sort
            {
                u = new BSUtility<T>(); //Delegate to the bubble sort Utility
            }
            else if (this.getSortName() == "Quick Sort")
            {
                u = new QSUtility<T>(); //Delegate to Quick sort Utility
            }

        }

        public override List<T> sort(List<T> data)
        {

            //Call delegated sort
            data = u.sort(data); // Sorts data  


            if (u is BSUtility<T>) //Check for Bubble Sort
            {
                
                foreach(T t in data) //Print logic for Bubble sort
                {
                    Console.WriteLine(t.getID() + " " + t.getName() + " " + t.getPrice());
                }
            }
            else if (u is QSUtility<T>)  //Check Quicksort
            {
                foreach (T t in data) //Print logic for Quicksort
                {
                    Console.WriteLine(t.getPrice() + " " + t.getName() + " " + t.getID());
                }
            }


            return data;
        }
    }

    class QSUtility<T> : Utility<T> where T : IComparable<T>, ProductIF
    {
        public override List<T> sort(List<T> data)
        {

            quickSort(data, 0, data.Count);
            return data;
        }


        /* Credit to https://codereview.stackexchange.com/questions/47128/quicksort-c-implementation for basis of algorithm */
        static int partition(List<T> list, int left, int right)
        {
            int start = left;
            T pivot = list[start];
            left++;
            right--;

            while (true)
            {
                while (left <= right && list[left].CompareTo(pivot) <= 0)// while(left is less/equal than pivot) list[left].getPrice() <= pivot.getPrice()
                    left++;

                while (left <= right && list[left].CompareTo(pivot) > 0)// while(left is greater than pivot list[right].getPrice() > pivot
                    right--;

                if (left > right)
                {
                    list[start] = list[left - 1];
                    list[left - 1] = pivot;

                    return left;
                }


                T temp = list[left];
                list[left] = list[right];
                list[right] = temp;

            }
        }

        static void quickSort(List<T> list, int left, int right)
        {
            if (list == null || list.Count <= 1)
                return;

            if (left < right)
            {
                int pivotIdx = partition(list, left, right);
                quickSort(list, left, pivotIdx - 1);
                quickSort(list, pivotIdx, right);
            }
        }


    }

    class BSUtility<T> : Utility<T> where T : IComparable<T>, ProductIF
    {

        public override List<T> sort(List<T> data)
        {


            //Bubble sort Algo
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data.Count - 1; j++)
                {
                    T temp1 = data[j];
                    T temp2 = data[j + 1];
                    if (temp1.CompareTo(temp2) > 0) //If first object greater than second object swap
                    {
                        //Swap
                        data[j+1] = temp1;
                        data[j] = temp2;
                    }
                }
            }

            return data; //returns List
        }

    }

    interface ProductIF
    {
        double getPrice();
        int getID();
        string getName();
    }

    class Desk : ProductIF, IComparable<Desk>   
    {
        private double price;
        private int ID;
        private String name; 

        // Constructor
        public Desk(double price, int ID, String name)
        {
            this.price = price;
            this.ID = ID;
            this.name = name;
        }
        
        //Implement CompareTo method from IComparable interface
        public int CompareTo(Desk other)
        {
            return price.CompareTo(other.getPrice()); //simple version
        }


        public double getPrice()
        {
            return price;
        }
        public void setPrice(double p)
        {
            this.price = p; 
        }

        public int getID() { return ID; }//getter for ID
        public String getName() { return name; } //getter for name
        public void setName(String name) { this.name = name; }// setter for name
        
    }

}
