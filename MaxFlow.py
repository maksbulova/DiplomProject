import sys


# C = sys.argv[1]
# source = sys.argv[1]
# sink = sys.argv[2]


# Dinic Algorithm

# build level graph by using BFS
def Bfs(C, F, s, t):  # C is the capacity matrix
    n = len(C)
    queue = []
    queue.append(s)
    global level
    level = n * [0]  # initialization
    level[s] = 1
    while queue:
        k = queue.pop(0)
        for i in range(n):
            if (F[k][i] < C[k][i]) and (level[i] == 0):  # not visited
                level[i] = level[k] + 1
                queue.append(i)
    return level[t] > 0


# search augmenting path by using DFS
def Dfs(C, F, k, cp):
    tmp = cp
    if k == len(C) - 1:
        return cp
    for i in range(len(C)):
        if (level[i] == level[k] + 1) and (F[k][i] < C[k][i]):
            f = Dfs(C, F, i, min(tmp, C[k][i] - F[k][i]))
            F[k][i] = F[k][i] + f
            F[i][k] = F[i][k] - f
            tmp = tmp - f
    return cp - tmp


# calculate max flow
# _ = float('inf')
def MaxFlow(C, s, t):
    n = len(C)
    F = [n * [0] for i in range(n)]  # F is the flow matrix
    flow = 0
    while (Bfs(C, F, s, t)):
        flow = flow + Dfs(C, F, s, 100000)
    return flow


'''
# make a capacity graph
# node s   o   p   q   r   t
C = [[ 0, 3, 3, 0, 0, 0 ],  # s
     [ 0, 0, 2, 3, 0, 0 ],  # o
     [ 0, 0, 0, 0, 2, 0 ],  # p
     [ 0, 0, 0, 0, 4, 2 ],  # q
     [ 0, 0, 0, 0, 0, 2 ],  # r
     [ 0, 0, 0, 0, 0, 3 ]]  # t
     
     
#      1  2  3  4
C = [[ 0, 0, 0, 0], #1
     [ 0, 0, 0, 0], #2
     [ 0, 0, 0, 0], #3
     [ 0, 0, 0, 0]] #4
     
#      1  2  3  4  5
C = [[ 0, 0, 0, 0, 0],  #1
     [ 0, 0, 0, 0, 0],  #2
     [ 0, 0, 0, 0, 0],  #3
     [ 0, 0, 0, 0, 0],  #4
     [ 0, 0, 0, 0, 0]]  #5
     
#      1  2  3  4  5  6
C = [[ 0, 0, 0, 0, 0, 0 ], #1
     [ 0, 0, 0, 0, 0, 0 ], #2
     [ 0, 0, 0, 0, 0, 0 ], #3
     [ 0, 0, 0, 0, 0, 0 ], #4
     [ 0, 0, 0, 0, 0, 0 ], #5
     [ 0, 0, 0, 0, 0, 0 ]] #6
'''

#      1  2  3  4
C = [[ 0, 8, 3, 0], #1
     [ 0, 0, 4, 4], #2
     [ 0, 0, 0, 6], #3
     [ 0, 0, 0, 0]] #4

source = 1 -1
sink = 4 -1



'''
#      1  2  3  4  5  6
C = [[ 0, 3, 3, 0, 0, 0 ],  # 1
     [ 0, 0, 2, 3, 0, 0 ],  # 2
     [ 0, 0, 0, 0, 2, 0 ],  # 3
     [ 0, 0, 0, 0, 4, 2 ],  # 4
     [ 0, 0, 0, 0, 0, 2 ],  # 5
     [ 0, 0, 0, 0, 0, 3 ]]  # 6

source = 1 -1
sink = 6 -1
'''

# print("Dinic's Algorithm")
max_flow_value = MaxFlow(C, source, sink)
print("max_flow_value is", max_flow_value)
