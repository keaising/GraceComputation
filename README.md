# GraceComputation
Computation for Grace

I use the library Math.NET Numerics to compute some Matraix for Grace's essay.
1. 完成算法
完成相关算法 
2. 数据导入导出
+ 数据导入：从excle导入，使用的Microsoft.Office.Interop.Excel
+ 数据导出：导出到json，使用的NewtonJson进行序列化，导出到excel，使用NPOI
3. 开发WindowsForm
为了方便使用，提供一定的进度提示（其实我觉得控制台的命令就挺好用了。。。
4. 并发、异步
最短路算法Floyd时间复杂度为O(N^3)，最外层循环采用并发，计算时间从108分钟减少到25分钟左右，勉强可以接受
窗体中添加异步代码，避免计算时窗体长时间假死 
All for Grace!
