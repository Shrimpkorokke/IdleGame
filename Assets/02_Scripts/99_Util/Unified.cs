using System;
using System.Globalization;
using System.Numerics;

/*
 Unified 구조체는 수치 값을 나타내기 위한 사용자 정의 타입입니다. 정수 (int, BigInteger)와 실수 (float) 타입의 값 모두를
 처리할 수 있는 역할을 합니다. 각 인스턴스는 IntPart, FloatPart, IsFloat 세 개의 필드를 가지고 있습니다.

 - IntPart: 인스턴스가 정수 값을 가질 때 해당 값을 저장합니다. 실수 값을 가진 경우에도 해당 실수 값을 BigInteger로 변환하여 저장합니다.
 - FloatPart: 인스턴스가 실수 값일 때 해당 값을 저장합니다. 정수 값을 가진 경우에도 해당 정수 값을 float로 변환하여 저장합니다.
 - IsFloat: 현재 인스턴스가 실수 값(float)을 가지고 있는지 여부를 나타내는 bool 필드입니다.

 연산자 오버로딩을 통해 + 와 * 그리고 IComparable 인터페이스를 구현하여 간단한 비교와 산술 연산도 제공합니다.
 IComparable<Unified> 인터페이스를 구현하여 Unified 타입 간의 비교를 가능하게 하였습니다.
 */
public struct Unified : IComparable<Unified>
{
    public BigInteger IntPart;
    public float FloatPart;
    public bool IsFloat;

    public Unified(int value)
    {
        IntPart = value;
        FloatPart = value;
        IsFloat = false;
    }

    public Unified(float value)
    { 
        IntPart = new BigInteger(value);
        FloatPart = value;
        IsFloat = true;
    }

    public Unified(BigInteger value)
    {
        IntPart = value;
        FloatPart = (float)value;
        IsFloat = false;
    }
    
    public static Unified operator *(Unified a, Unified b)
    {
        if (a.IsFloat || b.IsFloat)
        {
            return new Unified(a.FloatPart * b.FloatPart);
        }

        return new Unified(a.IntPart * b.IntPart);
    }

    public static Unified operator +(Unified a, Unified b)
    {
        if (a.IsFloat || b.IsFloat)
        {
            return new Unified(a.FloatPart + b.FloatPart);
        }

        return new Unified(a.IntPart + b.IntPart);
    }
    
    public static Unified operator -(Unified a, Unified b)
    {
        if (a.IsFloat || b.IsFloat)
        {
            return new Unified(a.FloatPart - b.FloatPart);
        }

        return new Unified(a.IntPart - b.IntPart);
    }

    public int CompareTo(Unified other)
    {
        // 둘 다 정수일 경우 정수 비교
        if (!IsFloat && !other.IsFloat)
        {
            return IntPart.CompareTo(other.IntPart);
        }

        // 그렇지 않은 경우 실수로 비교
        return FloatPart.CompareTo(other.FloatPart);
    }

    public override string ToString()
    {
        // float 값이 있을 경우 float 반환, 그렇지 않을 경우 BigInteger 반환
        return IsFloat ? FloatPart.ToString(CultureInfo.InvariantCulture) : IntPart.ToString();
    }
}