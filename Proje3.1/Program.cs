using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje3._1
{
    class Node
    {
        private int nüfus; // data item (key)
        private string mahalleAdi;
        // -------------------------------------------------------------
        public Node(int key,string mahalleAdı) // constructor
        {   
            nüfus = key;
            mahalleAdi = mahalleAdı;
        }
        // -------------------------------------------------------------
        public int getKey()
        { return nüfus; }
        public string getMahalleAdi()
        { return mahalleAdi; }
        // -------------------------------------------------------------
        public void setKey(int id)
        { nüfus = id; }
        // -------------------------------------------------------------
    } // end class Node

    class Heap
    {
        private Node[] heapArray;
        private int maxSize; // size of array
        private int currentSize; // number of nodes in array

        public Node[] getHeapArray()
        {
            return heapArray;
        }

        public Heap(int mx) // constructor
        {
            maxSize = mx;
            currentSize = 0;
            heapArray = new Node[maxSize]; // create array
        }

        public bool isEmpty()
        { return currentSize == 0; }

        public bool insert(string mahalleAdi, int key) // ekleme metodu
        {
            if (currentSize == maxSize)
                return false;
            Node newNode = new Node(key, mahalleAdi);
            heapArray[currentSize] = newNode;
            trickleUp(currentSize++);
            return true;
        } // end insert()

        public void trickleUp(int index)
        {
            int parent = (index - 1) / 2;
            Node bottom = heapArray[index];
            while (index > 0 && (heapArray[parent].getKey() < bottom.getKey())) //parent child dan küçük olduğu sürece
            {
                heapArray[index] = heapArray[parent]; // move it down
                index = parent;     //parent ile yer değiştirildi
                parent = (parent - 1) / 2;
            } // end while
            heapArray[index] = bottom;
        } // end trickleUp()

        public Node remove() // delete item with max key
        { // (assumes non-empty list)
            Node root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            trickleDown(0);
            return root;
        } // end remove()

        public void trickleDown(int index)
        {
            int largerChild;
            Node top = heapArray[index]; // save root
            while (index < currentSize / 2) // while node has at
            { // least one child,
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                // find larger child
                if (rightChild < currentSize && // (rightChild exists?)
                heapArray[leftChild].getKey() < heapArray[rightChild].getKey())
                    largerChild = rightChild;
                else
                    largerChild = leftChild;
                // top >= largerChild?
                if (top.getKey() >= heapArray[largerChild].getKey())
                    break;
                // shift child up
                heapArray[index] = heapArray[largerChild];
                index = largerChild; // go down
            } // end while
            heapArray[index] = top; // root to index
        } // end trickleDown()
        public bool change(int index, int newValue)
        {
            if (index < 0 || index >= currentSize)
                return false;
            int oldValue = heapArray[index].getKey(); // remember old
            heapArray[index].setKey(newValue); // change to new
            if (oldValue < newValue) // if raised,
                trickleUp(index); // trickle it up
            else // if lowered,
                trickleDown(index); // trickle it down
            return true;
        } // end change()

        public void displayHeap()
        {
            Console.WriteLine("Mahalleler"); // array format
            for (int m = 0; m < currentSize; m++)
                if (heapArray[m] != null)
                    Console.WriteLine(heapArray[m].getMahalleAdi() + " = "+ heapArray[m].getKey() );
                else
                    Console.WriteLine("--");
            Console.WriteLine();
            
        } // end displayHeap()
    } // end class Heap


    class ArrayIns
    {
        private long[] theArray; // ref to array theArray
        private int nElems; // number of data items
                            //--------------------------------------------------------------
        public ArrayIns(int max) // constructor
        {
            theArray = new long[max]; // create the array
            nElems = 0; // no items yet
        }
        //--------------------------------------------------------------
        public void insert(long value) // put element into array
        {
            theArray[nElems] = value; // insert it
            nElems++; // increment size
        }
        //--------------------------------------------------------------
        public void quickSort()
        {
            recQuickSort(0, nElems - 1);
        }
        //--------------------------------------------------------------
        public void recQuickSort(int left, int right)
        {
            if (right - left <= 0) // if size <= 1,
                return; // already sorted
            else // size is 2 or larger
            {
                long pivot = theArray[right]; // rightmost item
                                              // partition range
                int partition = partitionIt(left, right, pivot);
                recQuickSort(left, partition - 1); // sort left side
                recQuickSort(partition + 1, right); // sort right side
            }
        } // end recQuickSort()
          //--------------------------------------------------------------
        public int partitionIt(int left, int right, long pivot)
        {
            int leftPtr = left - 1; // left (after ++)
            int rightPtr = right; // right-1 (after --)
            while (true)
            { // find bigger item
                while (theArray[++leftPtr] < pivot)
                    ; // (nop)
                      // find smaller item
                while (rightPtr > 0 && theArray[--rightPtr] > pivot)
                    ; // (nop)
                if (leftPtr >= rightPtr) // if pointers cross,
                    break; // partition done
                else // not crossed, so
                    swap(leftPtr, rightPtr); // swap elements
            } // end while(true)
            swap(leftPtr, right); // restore pivot
            return leftPtr; // return pivot location
        } // end partitionIt()
          //--------------------------------------------------------------
        public void swap(int dex1, int dex2) // swap two elements
        {
            long temp = theArray[dex1]; // A into temp
            theArray[dex1] = theArray[dex2]; // B into A
            theArray[dex2] = temp; // temp into B
        } // end swap(
          //--------------------------------------------------------------
    } // end class ArrayIns



    internal class Program
    {
        static Random random = new Random();
        static Heap mahalleyiHeapeEkle(Heap mahalleYığını,string mahalleAdi,int nüfus)
        {
            mahalleYığını.insert(mahalleAdi, nüfus);

            return mahalleYığını;
        }
        static void nüfusuEnFazlaOlanÜçMahalleYazdir(Heap yığın)
        {
            Node[] array = yığın.getHeapArray();
            Console.WriteLine("Nüfusu en fazla olan 3 mahalle:");
            for(int i = 0; i < 3; i++)
            {
                Console.WriteLine((i+1) + ") " +yığın.remove().getMahalleAdi()+ " = "+ yığın.remove().getKey());
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            string[] mahalleBilgileri = { "Evka 4", "Naldöken", "Kemalpaşa", "Barbaros", "Meriç",
                 "Kızılay", "Mevlana", "Yeşilova", "Doğanlar", "Rafet Paşa"};
            int[] nüfus = { 14630, 9892, 11742, 11598, 8394, 15795, 25492, 31008, 21461, 19476 };
            Heap mahalleYığını = new Heap(10);

            for (int i = 0; i < 10;i++)
            {
                mahalleYığını = mahalleyiHeapeEkle(mahalleYığını, mahalleBilgileri[i],nüfus[i]);                 
            }
            //3.a soru 
            //mahalleYığını.displayHeap();
            //3.b soru
            nüfusuEnFazlaOlanÜçMahalleYazdir(mahalleYığını);

            //Soru 4.a MAIN
            int maxSize = 16; // array size
            ArrayIns arr;
            arr = new ArrayIns(maxSize); // create array
            for (int j = 0; j < maxSize; j++) // fill array with
            { // random numbers
                long n = (int)random.Next(99);
                arr.insert(n);
            }

            arr.quickSort(); // quicksort them
        
            Console.ReadLine();
        }
    }
}
