data = xlsread('results.xlsx', 'Sheet1', 'A2:C16');
x = data(:,1);
y = data(:,2);
z = data(:,3);
tri = delaunay(x,y);
trisurf(tri,x,y,z);