R = []
Left = []
Right = []
F = ['e','h','i']
G = ['a','b','c','d','g','j']

def Init(path,Left,Right):
    f = open(path)
    while True:
        string = f.readline()
        if not string:
            break
        string = string.replace('\t',' ')
        string = string.replace('\n','')
        a = string.split(' ')
        Left.append(a[0:-1])
        Right.append(a[-1])

def RBS():
    S = []
    m=0
    print(" -----------------------------------------------------------------------------------------------------")
    print("|  r  |               F               |               S               |               R               |")
    first = -1
    for k in range(1,14):
        R.append(k)  
    while m<14:
        m=m+1
        c = 0
        pos = 0
        for i in Left:
            count = 0
            pos = pos + 1
            lenR = len(i)
            for j in i:
                if j in F:
                    count = count + 1
                    if lenR == count:
                        Left[pos-1] = ['']
                        S.append(pos)
                        R.remove(pos)
        print(" -----------------------------------------------------------------------------------------------------")
        if first+1 == 0: 
            print('|','    ',end="|")
        else :
            if first + 1 > 9:
                print('|',first+1,end="  |")
            else :
                print('|',first+1,end="   |") 
        print(','.join(map(str,F)),' '*(30-2*len(F)+1),end='|')
        S1 = ','.join(map(str,S[::-1]))    
        print(S1,' '*(30-len(S1)),end='|')
        R1 = ','.join(map(str,R)) 
        print(R1,' '*(29-len(R1)),'|')
        if len(S)!=0:
            first = S.pop(0)-1
        else:
            first = 0
        if Right[first] not in F:
            F.append(Right[first])
    print(" -----------------------------------------------------------------------------------------------------")

def main():
    Init("suydientien.inp",Left,Right)
    RBS()

if(__name__ == "__main__"):
    main()


'''
a	c	h	b
b	h	a
b	h	c
b	h	d
a	c	i	b
a	b	c	i	d
a	d	e	i	b
a	d	e	i	c
a	d	e	i	g
c	g	i	a
c	g	i	j
h	b
h	c
'''