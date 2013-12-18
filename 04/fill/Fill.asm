// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, the
// program clears the screen, i.e. writes "white" in every pixel.

// Put your code here.
@16384
D=A;
@addrstart
M=D;

@filled
M=0

(LOOP)
@24576
D=M
@FILL
D;JGT
@CLEAR
0;JMP


(FILL)
@filled
D=M
@LOOP
D;JGT

@16384
D=A;
@addrstart
M=D;

(FILLSTART)
@addrstart
D=M
A=D
M=-1
@addrstart
M=M+1
D=M

@24576
D=A-D
@FILLEND
D;JEQ
@FILLSTART
0;JMP
(FILLEND)
@filled
M=1
@LOOP
0;JMP

(CLEAR)
@filled
D=M
@LOOP
D;JEQ

@16384
D=A;
@addrstart
M=D;

(CLEARSTART)
@addrstart
D=M
A=D
M=0
@addrstart
M=M+1
D=M

@24576
D=A-D
@CLEAREND
D;JEQ
@CLEARSTART
0;JMP
(CLEAREND)
@filled
M=0
@LOOP
0;JMP