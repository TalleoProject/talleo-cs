using System;
using System.Collections.Generic;

namespace Talleo
{

    public interface IFusionManager
    {

        public struct EstimateResult
        {
            public UInt64 fusionReadyCount;
            public UInt64 totalOutputCount;
        };

        public UInt64 createFusionTransaction(UInt64 threshold, UInt16 mixin, in List<string> sourceAddresses, in string destinationAddress);
        public bool isFusionTransaction(UInt64 transactionId);
        public EstimateResult estimate(UInt64 threshold, in List<string> sourceAddresses);
    };

}