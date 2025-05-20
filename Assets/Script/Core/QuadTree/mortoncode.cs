using System;
using System.IO;
using UnityEngine;
using System.Text;

public static class mortoncode
{
    private static readonly ushort[] Morton256X;
    private static readonly ushort[] Morton256Y;
    private static readonly uint[,] MortonTable;

    static mortoncode()
    {
        Morton256X = new ushort[256];
        Morton256Y = new ushort[256];
        MortonTable = new uint[100, 100];

        // Tính toán bảng Morton cho các giá trị từ 0 đến 255
        for (int i = 0; i < 256; i++)
        {
            Morton256X[i] = Part1By1((uint)i);
            Morton256Y[i] = (ushort)(Part1By1((uint)i) << 1);
        }

        // Tạo bảng Morton 2D cho các giá trị x, y từ 0 đến 99
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                MortonTable[x, y] = EncodeMorton2D(x, y);
            }
        }
    }

    // Hàm interleave bits để tính toán Morton code cho một chiều
    private static ushort Part1By1(uint x)
    {
        x &= 0x000000FF;
        x = (x | (x << 4)) & 0x0F0F;
        x = (x | (x << 2)) & 0x3333;
        x = (x | (x << 1)) & 0x5555;
        return (ushort)x;
    }

    // Hàm tạo Morton code 2D
    public static uint EncodeMorton2D(int x, int y)
    {
        return (uint)(Morton256X[x & 0xFF] | Morton256Y[y & 0xFF] << 1);
    }

    // Xuất bảng Morton ra file
    public static void ExportMortonTableToFile()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("// Morton Code Lookup Table");
        sb.AppendLine("// 0    1    100  101  1000 1001 1010 1011");
        sb.Append("{\n    ");

        // Xuất từng giá trị trong bảng Morton 2D
        for (int y = 0; y < 100; y++)
        {
            for (int x = 0; x < 100; x++)
            {
                sb.AppendFormat("0x{0:X4}, ", MortonTable[x, y]);
            }
            sb.Append("\n    ");
        }

        sb.AppendLine("\n};");

        // Lưu bảng vào file trong thư mục gốc của dự án Unity
        string path = Path.Combine(Application.dataPath, "MortonTable.txt");
        File.WriteAllText(path, sb.ToString());
        Debug.Log($"Morton table exported to {path}");
    }
}