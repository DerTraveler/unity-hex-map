﻿/* Copyright (c) 2016 Kevin Fischer
 *
 * This Source Code Form is subject to the terms of the MIT License.
 * If a copy of the license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT. */

using System;

namespace HexMapEngine {

    [System.Serializable]
    public struct Hex : IEquatable<Hex> {

        public int q, r;

        public int s { get { return -(q + r); } }

        public Hex(int q, int r) {
            this.q = q;
            this.r = r;
        }

        public int Length { get { return (Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2; } }

        public int DistanceTo(Hex other) {
            return (this - other).Length;
        }

        #region Related Hexes
        public Hex[] GetOffsetHexes(Hex[] offsets) {
            var result = new Hex[offsets.Length];
            for (int i = 0; i < offsets.Length; i++)
                result[i] = this + offsets[i];
            return result;
        }

        static Hex[] NEIGHBOR_OFFSETS = { 
            new Hex(1, 0), new Hex(0, 1), new Hex(-1, 1),
            new Hex(-1, 0), new Hex(0, -1), new Hex(1, -1)
        };

        public Hex[] GetNeighbors() {
            return GetOffsetHexes(NEIGHBOR_OFFSETS);
        }
        #endregion

        #region Operators
        // Implements IEquatable
        public bool Equals(Hex other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            if (obj is Hex)
                return Equals((Hex) obj);
            return false;
        }

        public override int GetHashCode() {
            return (51 + q.GetHashCode()) * 51 + r.GetHashCode();
        }

        public static bool operator==(Hex a, Hex b) {
            return a.q == b.q && a.r == b.r;
        }

        public static bool operator!=(Hex a, Hex b) {
            return !(a == b);
        }

        public static Hex operator+(Hex a, Hex b) {
            return new Hex(a.q + b.q, a.r + b.r);
        }

        public static Hex operator-(Hex a, Hex b) {
            return new Hex(a.q - b.q, a.r - b.r);
        }

        public static Hex operator-(Hex a) {
            return new Hex(-a.q, -a.r);
        }

        public static Hex operator*(Hex a, int k) {
            return new Hex(a.q * k, a.r * k);
        }
        #endregion

    }

}