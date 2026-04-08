namespace laba_aisd_1;

public class Graph
{
    private int[][] _data;
    
    public int Size { get;  }

    public void DoDfs()
    {
        var visited = new bool[Size];
        
        void Dfs(int u, bool[] visited)
        {
            visited[u] = true;
            Console.WriteLine("Посетил " + u);
            for (var i = 0; i < Size; i++)
            {
                if (_data[u][i] != 0 && !visited[i])
                {
                    Dfs(i, visited);
                }
            }
        }

        for (var i = 0; i < Size; i++)
        {
            if (!visited[i])
            {
                Dfs(i, visited);
            }
        }
    }
    
    public Tuple<int, int>[] Prim()
    {
        // минимальный остов содержит N-1 рёбер
        var ostov = new Tuple<int, int>[Size-1];
        
        // пара: дистанция : точка, из которой попадаешь
        // начнём с нулевой вершины и заполним для неё расстояния
        var distances = new Tuple<int, int>[Size];
        for (var i = 0; i < Size; i++)
        {
            if (_data[0][i] == 0)
                distances[i] = new Tuple<int, int>(Int32.MaxValue, -1);
            else
                distances[i] = new Tuple<int, int>(_data[0][i], 0);
        }

        var selectedVertexes = new bool[Size];
        
        var numberOfSelectedVertexes = 1;
        selectedVertexes[0] = true;
        
        while (numberOfSelectedVertexes < Size)
        {
            var v = ClosestVertex(distances, selectedVertexes);
            selectedVertexes[v] = true;
            ostov[numberOfSelectedVertexes - 1] = new Tuple<int, int>(v, distances[v].Item2);
            numberOfSelectedVertexes++;

            for (var i = 0; i < Size; i++)
            {
                if (!selectedVertexes[i] && _data[i][v] != 0 && distances[i].Item1 > _data[i][v])
                {
                    distances[i] = new Tuple<int, int>(_data[i][v], v);
                }
            }
        }

        return ostov;
    }
    
    public int ClosestVertex(Tuple<int, int>[] distances, bool[] selectedVertices)
    {
        var minLen = Int32.MaxValue;
        var vertex = 0;
        
        for (var i = 0; i < Size; i++)
        {
            var len = distances[i].Item1;
            if (!selectedVertices[i] && len != 0 && len < minLen)
            {
                minLen = len;
                vertex = i;
            }
        }

        if (minLen == Int32.MaxValue)
        {
            throw new Exception("Несвязный");
        }
        return vertex;
    }
    
    public void Print()
    {
        foreach (var line in _data)
        {
            foreach (var item in line)
            {
                Console.Write(item + " ");
            }
            Console.Write("\n");
        }
    }
    
    public Graph(string file)
    {
        using (StreamReader reader = new StreamReader(file))
        {
            Size = Int32.Parse(reader.ReadLine());
            _data = new int[Size][];
            for (var i = 0; i < Size; i++)
            {
                _data[i] = new int[Size];
            }
            
            var j = 0;
            while (reader.Peek() != -1)
            {
                var line = reader.ReadLine();

                for (var i = 0; i < line.Split(", ").Length; i++)
                {
                    _data[j][i] = Int32.Parse(line.Split(", ")[i]);
                }

                j++;
            }
        }
    }
    
    
    
}