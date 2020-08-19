using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteBytes
{
    public class A
    {
        public virtual void hi()
        {
            print("hi from a");
        }
    }

    public class B : A
    {
        public B()
        {
            base.hi();
            hi();
        }

        public override void hi()
        {
            print("hi from b");
        }

        public static int operator +(int a, B b)
        {
            return 2;
        }
    }

    // path to file
    static string path = "Assets/Scripts/Playground/FileHandling/ByteToText.txt";

    // [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        // should print hi from a and hit from b
        B b = new B(), b1 = new B();

        // operator overloading
        int a = 1 + b;
        print(a);



        unsafe
        {
            // FixedSpanExample();
            // int x = 5;
            // print("i was " + x);
            // Unsafe method: uses address-of operator (&):
            // SquarePtrParam(&x);
            // print("i is now " + x);

            // int length = 3;
            // int* numbers = stackalloc int[length];
            // print(sizeof(int));
            // print(sizeof(byte));
            // print(sizeof(char));
            // print(sizeof(float));
            // print(sizeof(bool));
            // print(sizeof(decimal));
            // print(sizeof(double));

            // for (var i = 0; i < length; i++)
            // {
            //     numbers[i] = i;
            //     print(numbers[i]);
            // }

            // int i = 10;

            // int* iptr = &i;
            // print((int)iptr);
            // print((int)&i);

            // char* nums = stackalloc char[5];
            // for (int i = 0; i < 5; ++i)
            // {

            //     // show that arrays are contiguous in memory
            //     print(*(nums + i));
            //     print((int)(nums + i));
            // }

            // print(nameof(nums));
        }
    }

    unsafe static void SquarePtrParam(int* p)
    {
        *p *= *p;
    }

    // unsafe private static void FixedSpanExample()
    // {
    //         int[] PascalsTriangle = {
    //                 1,
    //                 1,  1,
    //             1,  2,  1,
    //             1,  3,  3,  1,
    //         1,  4,  6,  4,  1,
    //         1,  5,  10, 10, 5,  1
    //     };

    //     Span<int> RowFive = new Span<int>(PascalsTriangle, 10, 5);

    //     fixed (int* ptrToRow = RowFive)
    //     {
    //         // Sum the numbers 1,4,6,4,1
    //         var sum = 0;
    //         for (int i = 0; i < RowFive.Length; i++)
    //         {
    //             sum += *(ptrToRow + i);
    //         }
    //         Console.WriteLine(sum);
    //     }
    // }




    #region old Main code
    // write stuff to the file in forms of bytes
    // using (FileStream stream = File.Open(path, FileMode.Create))
    // {
    // hex codes read from UTF-8 encoding table and Unicode characters
    // https://www.utf8-chartable.de/unicode-utf8-table.pl
    // byte[] bytes = new byte[] {
    //     0xc2,0xa1, 0x48, 0x6f, 0x6c, 0x61, 0x20, 0x4d, 0x75, 0x6e, 0x64, 0x6f, 0x21 };
    // Array.ForEach(bytes, stream.WriteByte);

    // or

    //     byte[] bytes = Encoding.UTF8.GetBytes("¡Hola Mundo!");
    //     stream.Write(bytes, 0, bytes.Length);
    // }

    // reads the file
    // using(FileStream stream = File.Open(path, FileMode.Open))
    // {

    //     byte[] buffer = new Byte[stream.Length];

    //     // read in the file contents and store in bytes
    //     stream.Read(buffer, 0, buffer.Length);

    //     string readInfo = Encoding.UTF8.GetString(buffer);

    //     print(readInfo);

    // }

    // using (FileStream stream = File.Open(path, FileMode.Create))
    // {
    // byte[] ar = SerializeVector(Vector3.zero);
    // stream.Write(ar, 0, ar.Length);
    // Vector3 readVector = DeserializeVector(ar);
    // print("read in vector coordinates: " + readVector);

    // formatter will serialize the vector3 into the file
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     formatter.Serialize(stream, new SerializableVector(Vector3.zero));
    // }

    // using (FileStream stream = File.Open(path, FileMode.Open))
    // {
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     var deserializedVector = (SerializableVector) formatter.Deserialize(stream);
    //     print(deserializedVector.ToVector());
    // }



    // using (StreamReader reader = new StreamReader(path))
    // {
    //     print(reader.ReadToEnd());
    // }

    // int x = 1;
    // switch (x)
    // {
    //     case int hi when x == 1:
    //         print("hi: " + hi);
    //         break;
    // }
    #endregion

    // static byte[] SerializeVector(Vector3 vector)
    // {
    //     int bytesPerFloat = 4;

    //     // 3 bc of x, y, and z
    //     byte[] bytes = new byte[3 * bytesPerFloat];

    //     // x will populate indices 0 thru 3
    //     BitConverter.GetBytes(vector.x).CopyTo(bytes,0);

    //     // x will populate indices 4 thru 7
    //     BitConverter.GetBytes(vector.y).CopyTo(bytes,4);

    //     // x will populate indices 8 thru 11
    //     BitConverter.GetBytes(vector.z).CopyTo(bytes,8);

    //     return bytes;
    // }

    // static Vector3 DeserializeVector(byte[] ar)
    // {
    //     Vector3 res = new Vector3();

    //     // for floats, will take 4 bytes at a time,
    //     // so this works
    //     res.x = BitConverter.ToSingle(ar, 0);
    //     res.y = BitConverter.ToSingle(ar, 4);
    //     res.z = BitConverter.ToSingle(ar, 8);

    //     return res;
    // }

    [Serializable]
    public struct SerializableVector
    {
        float x, y, z;
        public SerializableVector(Vector3 vector)
        {
            x = vector.x; y = vector.y; z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }

    static void print(object msg)
    {
        Debug.Log(msg);
    }
}
