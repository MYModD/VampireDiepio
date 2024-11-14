float3 hash3(float3 p)
{
    float3 q = float3(
        dot(p, float3(127.1, 311.7, 74.7)),
        dot(p, float3(269.5, 183.3, 246.1)),
        dot(p, float3(113.5, 271.9, 124.6))
    );
    return frac(sin(q) * 43758.5453123);
}

float2 voronoi(float3 p, float time, float cellSize)
{
    float3 n = floor(p * cellSize);
    float3 f = frac(p * cellSize);
    
    float2 closest = float2(2.0, 2.0);
    float3 mid_point = float3(0, 0, 0);
    
    // ���͂̃Z�����`�F�b�N
    for (int z = -1; z <= 1; z++)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                float3 offset = float3(x, y, z);
                float3 cell = n + offset;
                
                // �Z�����̃����_���ȓ_�𐶐�
                float3 points = hash3(cell);
                
                // �A�j���[�V����
                points = 0.5 + 0.5 * sin(time * float3(0.5, 0.7, 0.9) + 6.2831 * points);
                
                float3 diff = offset + points
                -f;
                float dist = length(diff);
                
                if (dist < closest.x)
                {
                    closest.y = closest.x;
                    closest.x = dist;
                    mid_point = cell + points;
                }
                else if (dist < closest.y)
                {
                    closest.y = dist;
                }
            }
        }
    }
    
    // �ł��߂��_�Ƃ̋����ƁA2�Ԗڂɋ߂��_�Ƃ̋����̍���Ԃ�
    return closest;
}

float smooth_voronoi(float3 p, float time, float cellSize, float smoothness)
{
    float2 v = voronoi(p, time, cellSize);
    
    // �Z���Ԃ̑J�ڂ��X���[�Y��
    float cells = 1.0 - smoothstep(0.0, smoothness, v.y - v.x);
    
    return cells;
}

float organic_pattern(float3 p, float time, float cellSize, float smoothness, float layerStrength)
{
    float pattern = 0.0;
    float strength = 1.0;
    float scale = 1.0;
    
    // �������C���[���d�ˍ��킹
    for (int i = 0; i < 3; i++)
    {
        pattern += smooth_voronoi(p * scale, time, cellSize, smoothness) * strength * layerStrength;
        strength *= 0.5;
        scale *= 2.0;
    }
    
    return pattern;
}

void OrganicVoronoi_float(
    float3 Position,
    float Time,
    float Scale, // �S�̓I�ȃX�P�[�� (����: 0.1-5.0)
    float CellSize, // �Z���̑傫�� (����: 1.0-10.0)
    float Smoothness, // �Z���Ԃ̊��炩�� (����: 0.1-0.5)
    float Speed, // �A�j���[�V�������x (����: 0.1-2.0)
    float LayerStrength, // ���C���[�̋��� (����: 0.5-2.0)
    float Contrast, // �R���g���X�g (����: 0.5-3.0)
    out float3 Out)
{
    float3 scaledPos = Position * Scale;
    float animTime = Time * Speed;
    
    // ���C���p�^�[���̐���
    float pattern = organic_pattern(
        scaledPos,
        animTime,
        CellSize,
        Smoothness,
        LayerStrength
    );
    
    // �R���g���X�g����
    pattern = pow(pattern, Contrast);
    
    // �o��
    Out = pattern.xxx;
}