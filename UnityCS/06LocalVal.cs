using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// c#은 고지식한 언어
// 클리스!
// 프로그렘의 시작도 Class 안에!

class Player
{
    int att;
}

namespace UnityCS
{
   
    internal class _06LocalVal
    {
        // 객체화 과정
        // 설계도를 이용해 객체를 만든다.
        Player newPlayer = new Player();

        void Figth()
        {
            // 지역 변수
            int att = 10;
        }
    }

    
}
