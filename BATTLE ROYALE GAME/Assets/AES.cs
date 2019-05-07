using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using UnityEngine;

public class AES: MonoBehaviour
    {

        //public static byte[] fullText;
        public static int[,] keys;
        public static System.String initKey;
        static int k = 0;
        public static System.String key = "";
        //public static System.String plainText = "";
        static int[,] Rcon = new int[,] {{0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36},
                                        {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
                                        {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
                                        {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}};
        public static int[,] sBox = {{(byte) 0x63, (byte) 0x7c, (byte) 0x77, (byte) 0x7b, (byte) 0xf2, (byte) 0x6b, (byte) 0x6f, (byte) 0xc5,
        (byte) 0x30, (byte) 0x01, (byte) 0x67, (byte) 0x2b, (byte) 0xfe, (byte) 0xd7, (byte) 0xab, (byte) 0x76},
    {(byte) 0xca, (byte) 0x82, (byte) 0xc9, (byte) 0x7d, (byte) 0xfa, (byte) 0x59, (byte) 0x47, (byte) 0xf0,
        (byte) 0xad, (byte) 0xd4, (byte) 0xa2, (byte) 0xaf, (byte) 0x9c, (byte) 0xa4, (byte) 0x72, (byte) 0xc0},
    {(byte) 0xb7, (byte) 0xfd, (byte) 0x93, (byte) 0x26, (byte) 0x36, (byte) 0x3f, (byte) 0xf7, (byte) 0xcc,
        (byte) 0x34, (byte) 0xa5, (byte) 0xe5, (byte) 0xf1, (byte) 0x71, (byte) 0xd8, (byte) 0x31, (byte) 0x15},
    {(byte) 0x04, (byte) 0xc7, (byte) 0x23, (byte) 0xc3, (byte) 0x18, (byte) 0x96, (byte) 0x05, (byte) 0x9a,
        (byte) 0x07, (byte) 0x12, (byte) 0x80, (byte) 0xe2, (byte) 0xeb, (byte) 0x27, (byte) 0xb2, (byte) 0x75},
    {(byte) 0x09, (byte) 0x83, (byte) 0x2c, (byte) 0x1a, (byte) 0x1b, (byte) 0x6e, (byte) 0x5a, (byte) 0xa0,
        (byte) 0x52, (byte) 0x3b, (byte) 0xd6, (byte) 0xb3, (byte) 0x29, (byte) 0xe3, (byte) 0x2f, (byte) 0x84},
    {(byte) 0x53, (byte) 0xd1, (byte) 0x00, (byte) 0xed, (byte) 0x20, (byte) 0xfc, (byte) 0xb1, (byte) 0x5b,
        (byte) 0x6a, (byte) 0xcb, (byte) 0xbe, (byte) 0x39, (byte) 0x4a, (byte) 0x4c, (byte) 0x58, (byte) 0xcf},
    {(byte) 0xd0, (byte) 0xef, (byte) 0xaa, (byte) 0xfb, (byte) 0x43, (byte) 0x4d, (byte) 0x33, (byte) 0x85,
        (byte) 0x45, (byte) 0xf9, (byte) 0x02, (byte) 0x7f, (byte) 0x50, (byte) 0x3c, (byte) 0x9f, (byte) 0xa8},
    {(byte) 0x51, (byte) 0xa3, (byte) 0x40, (byte) 0x8f, (byte) 0x92, (byte) 0x9d, (byte) 0x38, (byte) 0xf5,
        (byte) 0xbc, (byte) 0xb6, (byte) 0xda, (byte) 0x21, (byte) 0x10, (byte) 0xff, (byte) 0xf3, (byte) 0xd2},
    {(byte) 0xcd, (byte) 0x0c, (byte) 0x13, (byte) 0xec, (byte) 0x5f, (byte) 0x97, (byte) 0x44, (byte) 0x17,
        (byte) 0xc4, (byte) 0xa7, (byte) 0x7e, (byte) 0x3d, (byte) 0x64, (byte) 0x5d, (byte) 0x19, (byte) 0x73},
    {(byte) 0x60, (byte) 0x81, (byte) 0x4f, (byte) 0xdc, (byte) 0x22, (byte) 0x2a, (byte) 0x90, (byte) 0x88,
        (byte) 0x46, (byte) 0xee, (byte) 0xb8, (byte) 0x14, (byte) 0xde, (byte) 0x5e, (byte) 0x0b, (byte) 0xdb},
    {(byte) 0xe0, (byte) 0x32, (byte) 0x3a, (byte) 0x0a, (byte) 0x49, (byte) 0x06, (byte) 0x24, (byte) 0x5c,
        (byte) 0xc2, (byte) 0xd3, (byte) 0xac, (byte) 0x62, (byte) 0x91, (byte) 0x95, (byte) 0xe4, (byte) 0x79},
    {(byte) 0xe7, (byte) 0xc8, (byte) 0x37, (byte) 0x6d, (byte) 0x8d, (byte) 0xd5, (byte) 0x4e, (byte) 0xa9,
        (byte) 0x6c, (byte) 0x56, (byte) 0xf4, (byte) 0xea, (byte) 0x65, (byte) 0x7a, (byte) 0xae, (byte) 0x08},
    {(byte) 0xba, (byte) 0x78, (byte) 0x25, (byte) 0x2e, (byte) 0x1c, (byte) 0xa6, (byte) 0xb4, (byte) 0xc6,
        (byte) 0xe8, (byte) 0xdd, (byte) 0x74, (byte) 0x1f, (byte) 0x4b, (byte) 0xbd, (byte) 0x8b, (byte) 0x8a},
    {(byte) 0x70, (byte) 0x3e, (byte) 0xb5, (byte) 0x66, (byte) 0x48, (byte) 0x03, (byte) 0xf6, (byte) 0x0e,
        (byte) 0x61, (byte) 0x35, (byte) 0x57, (byte) 0xb9, (byte) 0x86, (byte) 0xc1, (byte) 0x1d, (byte) 0x9e},
    {(byte) 0xe1, (byte) 0xf8, (byte) 0x98, (byte) 0x11, (byte) 0x69, (byte) 0xd9, (byte) 0x8e, (byte) 0x94,
        (byte) 0x9b, (byte) 0x1e, (byte) 0x87, (byte) 0xe9, (byte) 0xce, (byte) 0x55, (byte) 0x28, (byte) 0xdf},
    {(byte) 0x8c, (byte) 0xa1, (byte) 0x89, (byte) 0x0d, (byte) 0xbf, (byte) 0xe6, (byte) 0x42, (byte) 0x68,
        (byte) 0x41, (byte) 0x99, (byte) 0x2d, (byte) 0x0f, (byte) 0xb0, (byte) 0x54, (byte) 0xbb, (byte) 0x16}};

        protected static byte[,] sBoxInv = {{(byte) 0x52, (byte) 0x09,
        (byte) 0x6a, (byte) 0xd5, (byte) 0x30, (byte) 0x36, (byte) 0xa5,
        (byte) 0x38, (byte) 0xbf, (byte) 0x40, (byte) 0xa3, (byte) 0x9e,
        (byte) 0x81, (byte) 0xf3, (byte) 0xd7, (byte) 0xfb}, {(byte) 0x7c,
        (byte) 0xe3, (byte) 0x39, (byte) 0x82, (byte) 0x9b, (byte) 0x2f,
        (byte) 0xff, (byte) 0x87, (byte) 0x34, (byte) 0x8e, (byte) 0x43,
        (byte) 0x44, (byte) 0xc4, (byte) 0xde, (byte) 0xe9, (byte) 0xcb},
    {(byte) 0x54, (byte) 0x7b, (byte) 0x94, (byte) 0x32, (byte) 0xa6,
        (byte) 0xc2, (byte) 0x23, (byte) 0x3d, (byte) 0xee, (byte) 0x4c,
        (byte) 0x95, (byte) 0x0b, (byte) 0x42, (byte) 0xfa, (byte) 0xc3,
        (byte) 0x4e}, {(byte) 0x08, (byte) 0x2e, (byte) 0xa1, (byte) 0x66,
        (byte) 0x28, (byte) 0xd9, (byte) 0x24, (byte) 0xb2, (byte) 0x76,
        (byte) 0x5b, (byte) 0xa2, (byte) 0x49, (byte) 0x6d, (byte) 0x8b,
        (byte) 0xd1, (byte) 0x25}, {(byte) 0x72, (byte) 0xf8, (byte) 0xf6,
        (byte) 0x64, (byte) 0x86, (byte) 0x68, (byte) 0x98, (byte) 0x16,
        (byte) 0xd4, (byte) 0xa4, (byte) 0x5c, (byte) 0xcc, (byte) 0x5d,
        (byte) 0x65, (byte) 0xb6, (byte) 0x92}, {(byte) 0x6c, (byte) 0x70,
        (byte) 0x48, (byte) 0x50, (byte) 0xfd, (byte) 0xed, (byte) 0xb9,
        (byte) 0xda, (byte) 0x5e, (byte) 0x15, (byte) 0x46, (byte) 0x57,
        (byte) 0xa7, (byte) 0x8d, (byte) 0x9d, (byte) 0x84}, {(byte) 0x90,
        (byte) 0xd8, (byte) 0xab, (byte) 0x00, (byte) 0x8c, (byte) 0xbc,
        (byte) 0xd3, (byte) 0x0a, (byte) 0xf7, (byte) 0xe4, (byte) 0x58,
        (byte) 0x05, (byte) 0xb8, (byte) 0xb3, (byte) 0x45, (byte) 0x06},
    {(byte) 0xd0, (byte) 0x2c, (byte) 0x1e, (byte) 0x8f, (byte) 0xca,
        (byte) 0x3f, (byte) 0x0f, (byte) 0x02, (byte) 0xc1, (byte) 0xaf,
        (byte) 0xbd, (byte) 0x03, (byte) 0x01, (byte) 0x13, (byte) 0x8a,
        (byte) 0x6b}, {(byte) 0x3a, (byte) 0x91, (byte) 0x11, (byte) 0x41,
        (byte) 0x4f, (byte) 0x67, (byte) 0xdc, (byte) 0xea, (byte) 0x97,
        (byte) 0xf2, (byte) 0xcf, (byte) 0xce, (byte) 0xf0, (byte) 0xb4,
        (byte) 0xe6, (byte) 0x73}, {(byte) 0x96, (byte) 0xac, (byte) 0x74,
        (byte) 0x22, (byte) 0xe7, (byte) 0xad, (byte) 0x35, (byte) 0x85,
        (byte) 0xe2, (byte) 0xf9, (byte) 0x37, (byte) 0xe8, (byte) 0x1c,
        (byte) 0x75, (byte) 0xdf, (byte) 0x6e}, {(byte) 0x47, (byte) 0xf1,
        (byte) 0x1a, (byte) 0x71, (byte) 0x1d, (byte) 0x29, (byte) 0xc5,
        (byte) 0x89, (byte) 0x6f, (byte) 0xb7, (byte) 0x62, (byte) 0x0e,
        (byte) 0xaa, (byte) 0x18, (byte) 0xbe, (byte) 0x1b}, {(byte) 0xfc,
        (byte) 0x56, (byte) 0x3e, (byte) 0x4b, (byte) 0xc6, (byte) 0xd2,
        (byte) 0x79, (byte) 0x20, (byte) 0x9a, (byte) 0xdb, (byte) 0xc0,
        (byte) 0xfe, (byte) 0x78, (byte) 0xcd, (byte) 0x5a, (byte) 0xf4},
    {(byte) 0x1f, (byte) 0xdd, (byte) 0xa8, (byte) 0x33, (byte) 0x88,
        (byte) 0x07, (byte) 0xc7, (byte) 0x31, (byte) 0xb1, (byte) 0x12,
        (byte) 0x10, (byte) 0x59, (byte) 0x27, (byte) 0x80, (byte) 0xec,
        (byte) 0x5f}, {(byte) 0x60, (byte) 0x51, (byte) 0x7f, (byte) 0xa9,
        (byte) 0x19, (byte) 0xb5, (byte) 0x4a, (byte) 0x0d, (byte) 0x2d,
        (byte) 0xe5, (byte) 0x7a, (byte) 0x9f, (byte) 0x93, (byte) 0xc9,
        (byte) 0x9c, (byte) 0xef}, {(byte) 0xa0, (byte) 0xe0, (byte) 0x3b,
        (byte) 0x4d, (byte) 0xae, (byte) 0x2a, (byte) 0xf5, (byte) 0xb0,
        (byte) 0xc8, (byte) 0xeb, (byte) 0xbb, (byte) 0x3c, (byte) 0x83,
        (byte) 0x53, (byte) 0x99, (byte) 0x61}, {(byte) 0x17, (byte) 0x2b,
        (byte) 0x04, (byte) 0x7e, (byte) 0xba, (byte) 0x77, (byte) 0xd6,
        (byte) 0x26, (byte) 0xe1, (byte) 0x69, (byte) 0x14, (byte) 0x63,
        (byte) 0x55, (byte) 0x21, (byte) 0x0c, (byte) 0x7d}
    };

        public AES(System.String initkey)
        {
            key = initkey;
            AES.generateKeys();
        }

        public void setInitKey(System.String key)
        {
            initKey = key;
        }


        public System.String getInitKey()
        {
            return initKey;
        }

     
        private static void initRound()
        {
            int count = 0;
            for (int i = 0; (i < 4); i++)
            {
                for (int j = 0; (j < 4); j++)
                {
                    keys[j, i] = ((byte)(key[count]));
                    count++;
                }

            }

        }

        private static int[] rotWord(int j)
        {
            int[] temp = new int[4];
            j--;
            temp[3] = keys[0, j];
            for (int i = 0; (i < 3); i++)
            {
                temp[i] = keys[(i + 1), j];
            }

            return temp;
        }

        private static void subByte(int j, int[] temp)
        {
            int r = 0;
            int c = 0;
            for (int i = 0; (i < 4); i++)
            {
                System.String s = temp[i].ToString("X");
                if ((s.Length < 2))
                {
                    r = 0;
                    c = fixHex(s[0]);
                }
                else
                {
                    c = fixHex(s[(s.Length - 1)]);
                    r = fixHex(s[(s.Length - 2)]);
                }

                temp[i] = sBox[r, c];
            }

        }

        public static int fixHex(char value)
        {
            int a = 0;
            switch (value)
            {
                case '0':
                    a = 0;
                    break;
                case '1':
                    a = 1;
                    break;
                case '2':
                    a = 2;
                    break;
                case '3':
                    a = 3;
                    break;
                case '4':
                    a = 4;
                    break;
                case '5':
                    a = 5;
                    break;
                case '6':
                    a = 6;
                    break;
                case '7':
                    a = 7;
                    break;
                case '8':
                    a = 8;
                    break;
                case '9':
                    a = 9;
                    break;
                case 'A':
                    a = 10;
                    break;
                case 'B':
                    a = 11;
                    break;
                case 'C':
                    a = 12;
                    break;
                case 'D':
                    a = 13;
                    break;
                case 'E':
                    a = 14;
                    break;
                case 'F':
                    a = 15;
                    break;
                case 'a':
                    a = 10;
                    break;
                case 'b':
                    a = 11;
                    break;
                case 'c':
                    a = 12;
                    break;
                case 'd':
                    a = 13;
                    break;
                case 'e':
                    a = 14;
                    break;
                case 'f':
                    a = 15;
                    break;

            }
            return a;
        }
        private static void generateKeys()
        {
            keys = new int[4, 44];
            int[] temp = new int[4];
            initRound();
            for (int j = 4; (j < 44); j++)
            {
                if (((j % 4) == 0))
                {
                    temp = rotWord(j);
                    subByte(j, temp);
                }

                for (int i = 0; (i < 4); i++)
                {
                    if (((j % 4) != 0))
                    {
                        keys[i, j] = (keys[i, (j - 4)] ^ keys[i, (j - 1)]);
                    }
                    else
                    {
                        keys[i, j] = ((temp[i] ^ Rcon[i, ((j / 4) - 1)]) ^ keys[i, (j - 4)]);
                    }

                }

            }
        }

        public static byte[,] shiftRows(byte[,] state)
        {
            byte[,] temp = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                temp[0, i] = state[0, i];
            }
            for (int i = 1; (i < 4); i++)
            {
                for (int j = 0; (j < 4); j++)
                {
                    temp[i, j] = state[i, ((j + i) % 4)];
                }

            }

            return temp;
        }

        public static byte[,] invShiftRows(byte[,] state)
        {
            byte[,] temp = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                temp[0, i] = state[0, i];
            }
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp[i, j] = state[i, (j - i + 4) % 4];
                }
            }
            return temp;
        }

        public static void addRoundKey(byte[,] bytematrix, int[,] keymatrix, int round)
        {
            for (int i = (round * 4); (i < ((round * 4) + 4)); i++)
            {
                for (int j = 0; (j < 4); j++)
                {
                    bytematrix[j, i % 4] ^= (byte)keys[j, i];
                }

            }
        }

        public static byte[,] mixColumns(byte[,] byteMatrix)
        {
            byte[,] stateNew = new byte[byteMatrix.Length, 4];
            for (int i = 0; (i < 4); i++)
            {
                stateNew[0, i] = xor4Bytes(multiply(byteMatrix[0, i], 2), multiply(byteMatrix[1, i], 3), byteMatrix[2, i], byteMatrix[3, i]);
                stateNew[1, i] = xor4Bytes(byteMatrix[0, i], multiply(byteMatrix[1, i], 2), multiply(byteMatrix[2, i], 3), byteMatrix[3, i]);
                stateNew[2, i] = xor4Bytes(byteMatrix[0, i], byteMatrix[1, i], multiply(byteMatrix[2, i], 2), multiply(byteMatrix[3, i], 3));
                stateNew[3, i] = xor4Bytes(multiply(byteMatrix[0, i], 3), byteMatrix[1, i], byteMatrix[2, i], multiply(byteMatrix[3, i], 2));
            }

            return stateNew;
        }

        private static byte[,] invMixColumns(byte[,] state)
        {
            byte[,] stateNew = new byte[state.Length, 4];
            for (int c = 0; c < 4; c++)
            {
                stateNew[0, c] = xor4Bytes(multiply(state[0, c], 0x0e),
                        multiply(state[1, c], 0x0b),
                        multiply(state[2, c], 0x0d),
                        multiply(state[3, c], 0x09));
                stateNew[1, c] = xor4Bytes(multiply(state[0, c], 0x09),
                        multiply(state[1, c], 0x0e),
                        multiply(state[2, c], 0x0b),
                        multiply(state[3, c], 0x0d));
                stateNew[2, c] = xor4Bytes(multiply(state[0, c], 0x0d),
                        multiply(state[1, c], 0x09),
                        multiply(state[2, c], 0x0e),
                        multiply(state[3, c], 0x0b));
                stateNew[3, c] = xor4Bytes(multiply(state[0, c], 0x0b),
                        multiply(state[1, c], 0x0d),
                        multiply(state[2, c], 0x09),
                        multiply(state[3, c], 0x0e));
            }
            return stateNew;
        }

        public static byte xor4Bytes(int b1, int b2, int b3, int b4)
        {
            byte bResult = 0;
            bResult = (byte)(bResult ^ b1);
            bResult = (byte)(bResult ^ b2);
            bResult = (byte)(bResult ^ b3); ;
            bResult = (byte)(bResult ^ b4); ;
            return bResult;
        }

        private static int multiply(byte v1, byte v2)
        {
            if (v1 < 0)
            {
                v1 = (byte)(v1 + 256);
            }
            System.String num = Convert.ToString(v1, 2);
            byte tem = v1;
            System.Text.StringBuilder sb = new System.Text.StringBuilder(num);
            byte b = 0x1b;
            while (sb.Length < 8)
            {
                sb.Insert(0, '0');
            }
            num = sb.ToString();
            bool mulb = false;
            if (num[(num.Length - 8)] == '1')
            {
                mulb = true;
            }
            if (v2 == 2)
            {

                v1 = (byte)((byte)(v1 << 1));
                if (mulb == true)
                {
                    v1 = (byte)(v1 ^ b);
                }
                return v1;
            }
            else if (v2 == 3)
            {
                v1 = (byte)((byte)(v1 << 1));
                if (mulb == true)
                {
                    v1 = (byte)(v1 ^ b);
                }
                v1 = (byte)((byte)v1 ^ tem);
                return v1;
            }
            else
            {

                byte[] bTemps = new byte[8];
                byte bResult = 0;
                bTemps[0] = v1;
                for (int i = 1; i < bTemps.Length; i++)
                {
                    bTemps[i] = xtime(bTemps[i - 1]);
                }
                for (int i = 0; i < bTemps.Length; i++)
                {
                    if (getBit(v2, i) != 1)
                    {
                        bTemps[i] = 0;
                    }
                    bResult ^= bTemps[i];
                }
                return bResult;
            }
        }

        private static byte getBit(byte value, int i)
        {
            byte[] bMasks = {(byte) 0x01, (byte) 0x02, (byte) 0x04,
            (byte) 0x08, (byte) 0x10, (byte) 0x20,
            (byte) 0x40, (byte) 0x80};
            byte bBit = (byte)(value & bMasks[i]);
            return (byte)((byte)(bBit >> i) & (byte)0x01);
        }

        private static byte xtime(byte value)
        {
            int iResult = 0;
            iResult = (int)(value & 0x000000ff) * 02;
            return (byte)(((iResult & 0x100) != 0) ? iResult ^ 0x11b : iResult);
        }

        private static byte[,] subBytes(byte[,] state)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = sboxTransform(state[i, j]);
                }
            }
            return state;
        }

        private static byte sboxTransform(byte value)
        {
            byte bUpper = 0, bLower = 0;
            bUpper = (byte)((byte)(value >> 4) & 0x0f);
            bLower = (byte)(value & 0x0f);
            return (byte)sBox[bUpper, bLower];
        }

        private static byte[,] invSubBytes(byte[,] state)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = invSboxTransform(state[i, j]);
                }
            }
            return state;
        }

        private static byte invSboxTransform(byte value)
        {
            byte bUpper = 0, bLower = 0;
            bUpper = (byte)((byte)(value >> 4) & 0x0f);
            bLower = (byte)(value & 0x0f);
            return sBoxInv[bUpper, bLower];
        }

        public static byte[,] encrypt(byte[,] byteMatrix)
        {
            byte[,] state = new byte[4, 4];
            state = byteMatrix;
            addRoundKey(state, keys, 0);
            for (int round = 1; round < 10; round++)
            {
                state = subBytes(state);
                state = shiftRows(state);
                state = mixColumns(state);
                addRoundKey(state, keys, round);
            }
            state = subBytes(state);
            state = shiftRows(state);
            addRoundKey(state, keys, 10);
            return state;
        }

        public static byte[,] decrypt(byte[,] byteMatrix)
        {
            byte[,] state = new byte[4, 4];
            state = byteMatrix;
            addRoundKey(state, keys, 10);
            state = invShiftRows(state);
            state = invSubBytes(state);
            for (int round = 9; round >= 1; round--)
            {
                addRoundKey(state, keys, round);
                state = invMixColumns(state);
                state = invShiftRows(state);
                state = invSubBytes(state);
            }
            addRoundKey(state, keys, 0);
            return state;
        }
        public static byte[,] setBlock(byte[] fullText, int k)
        {
            byte[,] block = new byte[4, 4];
            block = arrayToMatrix(fullText, k);
            return block;
        }

        public static byte[,] arrayToMatrix(byte[] fullText, int k)
        {
            byte[,] temp = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp[j, i] = fullText[k++];
                }
            }
            return temp;
        }
        public static System.String encrypting(System.String plainText)
        {
        byte[] fullText;
            if ((plainText.Length % 16) == 0)
            {
                fullText = new byte[plainText.Length];
                fullText = Encoding.ASCII.GetBytes(plainText);
            }
            else
            {
                plainText = padding(plainText);
                fullText = new byte[plainText.Length];
                fullText = Encoding.ASCII.GetBytes(plainText);
          
            }
            System.String toReturn = "";
            byte[,] state = new byte[4, 4];
            for (int i = 0; i < fullText.Length; i += 16)
            {
                state = setBlock(fullText, i);
                state = encrypt(state);
                toReturn += arrayToString(state);
            }
            return toReturn;
        }
        public static System.String arrayToString(byte[,] byteMatrix)
        {
            System.String ret = "";
            System.String temp = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (byteMatrix[j, i].ToString("X").Length == 2)
                    {
                        ret = ret + byteMatrix[j, i].ToString("X");
                    }
                    else if (byteMatrix[j, i].ToString("X").Length == 1)
                    {
                        ret = ret + "0" + byteMatrix[j, i].ToString("X");
                    }
                    else
                    {
                        temp = byteMatrix[j, i].ToString("X");
                        ret = ret + temp.Substring(temp.Length - 2, 2);
                    }
                }
            }
            return ret;
        }
        public static System.String decrypting(System.String cipherText)
        {

            byte[] temp = new byte[cipherText.Length / 2];

            for (int i = 0; i < temp.Length; i++)
            {
                int index = i * 2;

                int j = int.Parse(cipherText.Substring(index, 2), System.Globalization.NumberStyles.HexNumber);
                temp[i] = (byte)j;
            }
            System.String toReturn = "";
            byte[,] state = new byte[4, 4];
            for (int i = 0; i < temp.Length; i += 16)
            {
                state = setBlock(temp, i);
                state = decrypt(state);
                toReturn += arrayToString(state);
            }
            for (int i = 0; i < temp.Length; i++)
            {
                int index = i * 2;
                int j = int.Parse(toReturn.Substring(index, 2), System.Globalization.NumberStyles.HexNumber);
                temp[i] = (byte)j;
                Console.Write(temp[i] + "\t");
            }
            System.String s = System.Text.Encoding.UTF8.GetString(temp);
            s = deletePadding(s);
            return s;

        }
        public static System.String padding(System.String plainText)
        {
            int i = 0;
            while ((plainText.Length % 16) != 0)
            {
                if (i == 0)
                {
                    plainText += "@";
                    i++;
                }
                else
                {
                    plainText += "0";
                }
            }
            return plainText;
        }
        public static System.String deletePadding(System.String input)
        {

            bool hasPadding = false;
            int a = input.Length - 1;
            if (input[a] == '0' || input[a] == '@')
            {
                hasPadding = true;
            }

            if (!hasPadding)
            {
                return input;
            }

            for (int i = input.Length - 1; i > input.Length - 15; i--)
            {
            Debug.Log(i);

            if (input[i] == '0')
                {
                
                    input = input.Substring(0, input.Length -1);

                }
                else if (input[i] == '@')
                {
                    input = input.Substring(0, input.Length - 1);
                    return input;
                }
            }
            return input;
        }

        public static byte[,] setBlockDecrypt(byte[] fullText, int k)
        {
            byte[,] block = new byte[4, 4];
            block = arrayToMatrixDecrypt(fullText, k);
            return block;
        }
        public static byte[,] arrayToMatrixDecrypt(byte[] fullText, int k)
        {
            byte[,] temp = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp[j, i] = fullText[k++];
                }
            }

            return temp;
        }


    
}
