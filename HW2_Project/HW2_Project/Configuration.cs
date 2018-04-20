using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Project
{
    static class Configuration
    {
        public enum StepSizeMutationType : byte
        {
            Fixed, UnCorrelated, OneFifthRule
        }

        // using single stepsize of n step sizes
        public static bool s_isUsingSingleStepSize = true;
        // false - fixed step size(s) , true - step size(s) will change with mutation
        //public static bool s_isUsingCorrelatedMutation = false;
        // use this to determine the operation on step size
        public static StepSizeMutationType s_stepSizeMutationType = StepSizeMutationType.OneFifthRule;

        public static double s_initialStepSize = 1;
        // used for Uncorrelated Mutation
        public static double s_constantT = 0.02;
        public static double s_constantTprime = s_constantT * s_constantT;
        public static double s_stepSizeLowerBound = s_initialStepSize / 2.0;

        // used for 1/5 rule
        public static double s_constantA = 0.85;

        // false - (1,1) ES , true - (1+1) ES
        public static bool s_isParentCandidate = true;
    }
}