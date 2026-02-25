namespace SadTabletop.Shared.Helps;

public static class MehExtensions
{
    // :) мне плохо

    /// <summary>
    /// Возвращает уникальный случайный номер, учитывая номера из списка.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="get"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int NonRepeatedRandomGet<T>(this IList<T> list, Func<T, int> get)
    {
        int sample;
        do
        {
            sample = Random.Shared.Next(int.MinValue, int.MaxValue);
        } while (list.Select(get).Contains(sample));

        return sample;
    }

    /// <summary>
    /// Устанавливает предмету уникальный случайный номер, учитывая номера из списка 
    /// </summary>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <typeparam name="T"></typeparam>
    public static void NonRepeatedRandom<T>(this IList<T> list, T item, Func<T, int> get, Action<T, int> set)
    {
        int sample = NonRepeatedRandomGet(list, get);

        set(item, sample);
    }

    /// <summary>
    /// Присваивает всем элементам в листе уникальный случайный номер.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="set"></param>
    /// <typeparam name="T"></typeparam>
    public static void NonRepeatedRandomAssign<T>(this IList<T> list, Action<T, int> set)
    {
        int[] samples = list.Select(_ => Random.Shared.Next(int.MinValue, int.MaxValue)).ToArray();

        for (int i = 0; i < samples.Length; i++)
        {
            int sample = samples[i];

            if (samples.Skip(i + 1).All(s => s != sample))
                continue;

            do
            {
                sample = Random.Shared.Next(int.MinValue, int.MaxValue);
            } while (samples.Contains(sample));

            samples[i] = sample;
        }

        for (int i = 0; i < samples.Length; i++)
        {
            int sample = samples[i];
            T item = list[i];

            set(item, sample);
        }
    }
}