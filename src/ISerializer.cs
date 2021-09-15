using System;

namespace Talleo
{

    public interface ISerializer
    {
        void serialize<T>(ref T item, String field);
        void serializeAsBinary<T>(ref T item, String field);
    };

};
