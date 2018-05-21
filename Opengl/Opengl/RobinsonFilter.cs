using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opengl
{
    class RobinsonFilter
    {
        public static int[,] E = {
                                    {-1, 0, 1},
                                    {-2, 0, 2},
                                    {-1, 0, 1}
                                };
        public static int[,] NE = {
                                    { 0,  1, 2},
                                    {-1,  0, 1},
                                    {-2, -1, 0}
                                };
        public static int[,] N = {
                                    { 1,  2,  1},
                                    { 0,  0,  0},
                                    {-1, -2, -1}
                                };
        public static int[,] NW = {
                                    { 2,  1,  0},
                                    { 1,  0, -1},
                                    { 0, -1, -2}
                                };
        public static int[,] W = {
                                    {1, 0, -1},
                                    {2, 0, -2},
                                    {1, 0, -1}
                                };
        public static int[,] SW = {
                                    {0, -1, -2},
                                    {1,  0, -1},
                                    {2,  1,  0}
                                };
        public static int[,] S = {
                                    {-1, -2, -1},
                                    { 0,  0,  0},
                                    { 1,  2,  1}
                                };
        public static int[,] SE = {
                                    {-2,-1, 0},
                                    {-1, 0, 1},
                                    { 0, 1, 2}
                                };

        public static int[,] W_LP1 = {
                                    {1,1, 1},
                                    {1, 1, 1},
                                    { 1, 1, 1}
                                };
        public static int[,] W_LP2 = {
                                    {0,1, 0},
                                    {1, 2, 1},
                                    { 0, 1, 0}
                                };
        public static int[,] W_LP3 = {
                                    {1,2, 1},
                                    {2, 4, 2},
                                    { 1, 2, 1}
                                };
    }
}
