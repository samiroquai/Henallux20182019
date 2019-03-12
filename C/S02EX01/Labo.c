#include <stdio.h>
#include <string.h>
#include <stdbool.h>


bool estHexadecimal(char *input){
    char firstChar=  *input;
    if(firstChar!='0')
        return false;
    input++;
    char secondChar=*input;
    if(secondChar!='X' && secondChar!='x')
        return false;
    input++;
    while(*input!='\0' 
    && (isdigit(*input) || 
        (*input>='A' && *input<='F') ||
        (*input>='a' && *input<='f'))){
        input++;
    }
    return *input=='\0';
}

void main(){

    if(!estHexadecimal("0X123FE456"))
        puts("Le cas 0X123FE456 n'a pas été reconnu comme hexadecimal");
    if(estHexadecimal("0XZEOOAZEI"))
        puts("Le cas 0XZEOOAZEI a été reconnu à tort comme hexadecimal");
    if(estHexadecimal("0XEFOAZEI"))
        puts("Le cas 0XEFOAZEI a été reconnu à tort comme hexadecimal");
    if(estHexadecimal("0X123EFOAZEI"))
        puts("Le cas 0X123EFOAZEI a été reconnu à tort comme hexadecimal");
    if(!estHexadecimal("0X"))
        puts("Le cas 0X n'a pas été reconnu comme hexadecimal");
    if(estHexadecimal("OX"))
        //!!! c'est la lettre O
        puts("Le cas OX a été reconnu à tort comme hexadecimal");
    if(estHexadecimal("AX123"))
        puts("Le cas AX123 a été reconnu à tort comme hexadecimal");
    if(estHexadecimal("1X123"))
        puts("Le cas 1X123 a été reconnu à tort comme hexadecimal");
    if(estHexadecimal("0B123"))
        puts("Le cas 0B123 a été reconnu à tort comme hexadecimal");
    if(estHexadecimal("0123"))
        puts("Le cas 0123 a été reconnu à tort comme hexadecimal");
    if(estHexadecimal("123"))
        puts("Le cas 123 a été reconnu à tort comme hexadecimal");

}
