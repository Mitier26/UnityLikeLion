namespace _07ClassAccess
{
    class Player
    {
        public int att;
        int hp;

        public void Move()
        {
            Console.Write("움직인다");
        }
    }

    // 캡슐화 은닉화 
    // 접근제산 지정자
    // public
    // private
    // protected

    internal class Program
    {
        static void Main(string[] args)
        {
            Player newPlayer = new Player();

            newPlayer.att = 10;

            newPlayer.Move();
        }
    }
}
