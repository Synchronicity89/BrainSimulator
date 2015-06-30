﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using YAXLib;
using BrainSimulator.Memory;
using BrainSimulator.Nodes;
using BrainSimulator.Task;
using BrainSimulator.Utils;
using BrainSimulator.NeuralNetwork.Group;
using BrainSimulator.NeuralNetwork.Layers;
using BrainSimulator.NeuralNetwork.Tasks;


namespace BrainSimulator.LSTM.Tasks
{
    /// <summary>Dummy task, in truncated RTRL deltas are not propagated backwards from LSTM layer to input layer.</summary>
    [Description("Delta backprop"), MyTaskInfo(OneShot = false)]
    public class MyLSTMDummyDeltaTask : MyAbstractBackDeltaTask<MyAbstractLayer>
    {
        public MyLSTMDummyDeltaTask() { }
        public override void Init(int nGPU) { }
        public override void Execute()
        {
            // error is not propagated backwards from LSTM layer
        }
    }

    /// <summary>Computes deltas of output gates and cell state errors.</summary>
    [Description("Calculate deltas"), MyTaskInfo(OneShot = false)]
    public class MyLSTMDeltaTask : MyTask<MyLSTMLayer>
    {
        private MyCudaKernel m_deltaKernel;

        public override void Init(int nGPU)
        {
            m_deltaKernel = MyKernelFactory.Instance.Kernel(nGPU, @"LSTM\LSTMDeltaKernel", "LSTMDeltaKernel");
            m_deltaKernel.SetupExecution(Owner.MemoryBlocks);
        }

        public override void Execute()
        {
            if (SimulationStep == 0) return;

            m_deltaKernel.Run(
                Owner.CellStateErrors,
		        Owner.OutputGateDeltas,
		        Owner.CellStates,
		        Owner.OutputGateActivations,
		        Owner.OutputGateActivationDerivatives,
		        Owner.Delta,

		        Owner.CellStates.Count,
		        Owner.CellsPerBlock
                );
        }
    }
}