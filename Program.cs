using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

            
        }
    }

    class XYZ {

        private Utility<Desk> u;
        private List<Desk> l;

        public void setList(List<Desk> l) { this.l = l; }

        public void askUtility()
        {
            //Logic for assigning Utility
            u = new Utility<Desk>();
        }
    }

    class Utility<T> {
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

        public List<T> sort(List<T> data)
        {
            
            //Bubble sort Algo
            for(int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data.Count; j++)
                {
                    T temp1 = data[j]; //get first desk
                    T temp2 = data[j + 1];
                    if(temp1.CompareTo(temp2) > 0)
                    {

                    }
                }
            }
            return data; //stubbed to return list
        }

        /**
         * Getter method for sortName attribute
         */
        public String getSortName() { return sortName; }
        
    }

    interface ProductIF
    {
        double getPrice();
    }

    class Desk : ProductIF, IComparable<Desk>   //I was a little confused if we were supposed to create a new type list or just this desk? Just Desk seemed to make more sense
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
