using System;
namespace 编译原理实验
{
    class Program
    {
        static bool isalpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
        static bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }
        static void Main(string[] args)
        {
            string str=Console.ReadLine();
            if(isvalid(str) )
            {
                Console.WriteLine("successful");
            }
            else
            {
                Console.WriteLine("failed");
            }

        }
        static public bool isvalid(string str)
        {
            int pre_state = 0, now_state=0;
            string res="";
            foreach(char s in str)
            {
                now_state = move(s,pre_state);
                if(now_state == 19)
                {
                    if (!print(pre_state, res)){
                        return false;
                    }
                    res=String.Empty;
                    pre_state = 0;
                    now_state= move(s,pre_state);
                }
                res += s;
                pre_state=now_state;
            }
            if(now_state==19&& !print(pre_state, res) || !print(now_state, res))
            {
                return false;
            }
            return true;
        }
        static public bool print(int state,string res)
        {
            bool flag = false;
            if (isConstant(state))
            {
               Console.WriteLine ( "<Constant\t "+ res+ "\t>");
                flag = true;
            }
            else if (isVariable(state))
            {
                // 字符不超过32个
                if (res.Length > 32)
                {
                    Console.WriteLine( "变量长度超过了32个字符");
                }
                else
                {
                    Console.WriteLine("<Variable\t " + res + "\t>");
                    
                    flag = true;
                }
            }
            else if (isKeyword(state))
            {
                Console.WriteLine("<Keyword\t " + res + "\t>");
                
                flag = true;
            }
            else if (isOperation(state))
            {
                Console.WriteLine("<Operation\t " + res + "\t>");
                
                flag = true;
            }
            else if (isDelimiter(state))
            {
                Console.WriteLine("<Delimiter\t " + res + "\t>");
               
                flag = true;
            }
            return flag;
        }

        /* 是否为常量 */
        static public bool isConstant(int state)
        {
            return (state == 4 || state == 5 || state == 6 || state == 7);
        }

        /* 是否为变量 */
        static public bool isVariable(int state) { return state == 17; }

        /* 是否为关键字 */
        static public bool isKeyword(int state) { return state == 16; }

        /* 是否为运算符 */
        static public bool isOperation(int state) { return (state == 1 || state == 8); }

        /* 是否为分隔符 */
        static public bool isDelimiter(int state) { return state == 9; }
        //状态转移函数
        static public  int move(char chr,int state)
        {
            switch (state)
            {
                case 0:
                    if (chr == '+' || chr == '-')
                    {
                        return 1;
                    }
                    else if (chr == '*' || chr == '/' || chr == '^' || chr == '=')
                    {
                        return 8;
                    }
                    else if (chr == ';' || chr == '(' || chr == ')' || chr == ' ' || chr == ',')
                    {
                        return 9;
                    }
                    else if (chr == '\'')
                    {
                        return 18;
                    }
                    else if (chr == 'P')
                    {
                        return 2;
                    }
                    else if (chr == 'E')
                    {
                        return 4;
                    }
                    else if (chr == '0')
                    {
                        return 5;
                    }
                    else if (chr >= '1' && chr <= '9')
                    {
                        return 6;
                    }
                    else if (chr == 's')
                    {
                        return 10;
                    }
                    else if ((chr == 'c'))
                    {
                        return 11;
                    }
                    else if (chr == 't')
                    {
                        return 12;
                    }
                    else if (chr == 'l')
                    {
                        return 13;
                    }
                    else if (chr == '_' || isalpha(chr) && chr != 'P' && chr != 'E' && chr != 's' && chr != 'c' && chr != 't' && chr != 'l')
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 1:
                    return 19;
                    break;
                case 2:
                    if (chr == 'I')
                    {
                        return 4;
                    }
                    else if (chr == '_' || isalpha(chr) || isDigit(chr) && chr != 'I')
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 3:
                    if (chr == '0')
                    {
                        return 3;
                    }
                    else if (chr >= '1' && chr <= '9')
                    {
                        return 7;
                    }
                    else
                    {
                        return 19;
                    } break;
                case 4:
                    if (chr == '_' || isalpha(chr) || isDigit(chr))
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 5:
                    if (chr == '.')
                    {
                        return 3;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 6:
                    if (chr == '0')
                    {
                        return 6;
                    }
                    else if (chr >= '1' && chr <= '9')
                    {
                        return 6;
                    }
                    else if (chr == '.')
                    {
                        return 3;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 7:
                    if (chr == '0')
                    {
                        return 3;
                    }
                    else if (chr >= '1' && chr <= '9')
                    {
                        return 7;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 8:
                    return 19;
                    break;
                case 9:
                    return 19;
                    break;
                case 10:
                    if (chr == 'i')
                    {
                        return 14;
                    }
                    else if (chr == '_' || isalpha(chr) || isDigit(chr) && chr != 'i')
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 11:
                    if (chr == 't')
                    {
                        return 12;
                    }
                    else if (chr == 'o')
                    {
                        return 15;
                    }
                    else if (chr == '_' || isalpha(chr) || isDigit(chr) && chr != 'o' && chr != 't')
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 12:
                    if (chr == 'g')
                    {
                        return 16;
                    }
                    else if (chr == '_' || isalpha(chr) || isDigit(chr) && chr != 'g')
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 13:
                    if (chr == 'o')
                    {
                        return 12;
                    }
                    else if (chr == 'g' || chr == 'n')
                    {
                        return 16;
                    }
                    else if (chr == '_' || isalpha(chr) || isDigit(chr) && chr != 'o' && chr != 'g' && chr != 'n')
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 14:
                    if (chr == 'n')
                    {
                        return 16;
                    }
                    else if (chr == '_' || isalpha(chr) || isDigit(chr) && chr != 'n')
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 15:
                    if (chr == 's')
                    {
                        return 16;
                    }
                    else if (chr == '_' || isalpha(chr) || isDigit(chr) && chr != 's')
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 16:
                    if (chr == '_' || isalpha(chr) || isDigit(chr))
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 17:
                    if (chr == '_' || isalpha(chr) || isDigit(chr))
                    {
                        return 17;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 18:
                    if (chr == 't' || chr == 'n')
                    {
                        return 9;
                    }
                    else
                    {
                        return 19;
                    }
                    break;
                case 19:
                    return 19;
                    break;
                default:
                    return 19;
            }





            
        }
    }
}