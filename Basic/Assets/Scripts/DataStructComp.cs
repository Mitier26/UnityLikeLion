using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructComp : MonoBehaviour
{
    // 기본 데이터 처리 방식
    // 0 과 1 로 표현한다. 2 진법
    // 전기가 흐르면 1 전기가 흐르지 않으면 0
    // bit -> 컴퓨터 데이터 단위 표현의 최소 단위
    // byte -> bit 8개가 모인것 ( 00000000 )
    
    // 9 / 2 -> 4 % 1
    // 4 / 2 -> 2 % 0
    // 2 / 2 -> 1 % 0
    // 1 / 2 -> 0 % 1
    
    // 7 / 2 -> 3 % 1
    // 3 / 2 -> 1 % 1
    // 1 / 2 -> 0 % 1
    
    // 256 / 2 = 128 % 0
    // 128 / 2 = 64 % 0
    // 64 / 2 = 32 % 0
    // 32 / 2 = 16 % 0
    // 16 / 2 = 8 % 0
    // 8 / 2 = 4 % 0
    // 4 / 2 = 2 % 0
    // 2 / 2 = 1 % 0
    // 1 /2 = 0 % 1
    
    
    // 기본 자료형
    private int intValue = 0;               // 자료형 -21 ~ 22억 까지 표현 가능
    private UInt32 unitValue = 0;           // 0 ~ 42억 까지 표현 가능

    private Int64 longValue = 0;            // 매우 큰수... 
    private UInt64 ulongValue = 0;          // unit64 또는 longlong(다른 언어)
    
    // u 가 붙어있으면 음수가 없고 양수만 있는 것

    private float floatValue = 0.0f;        // 소수점 7자리 까지 표현, 1.23456789 ( 다음 소수점 아래는 미보장 )
    // 우주 공학이낭 정교한 데이터를 다루어 한다면?
    private double doubleValue = 0.0;       // 소수점 15자리 까지 표현
    
    bool boolValue = false;                 // true , false
    
    string stringValue = "안녕하세요";      // 문자열을 담음
    
    
}
