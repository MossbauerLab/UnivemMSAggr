# UnivemMSAggr
#1 General

A simple GUI (WPF) tool for fast data (Mossbauer spectra fits from UnivemMS software) aggregtion from couple of UNIVEM MS files into one simple text or doc files

It allows:
 to select components file for merging them into one report (docx or txt files) into tables with components sorting by 
  hyperfine field and quadrupol splitting (descending and ascending order, however selection in WPF app is not implemented yet)
- automatically calculate more suitable error for each Mossbauer parameter or leave it blank if calculation is not possible
     - for IS, QS and Heff is +-1 channel (mm/s or kOe)
     - for G is +- 2 channels
     - for S (relative area) is +-10%

As a result this software saving me a lot of time on searching and copying every value with proper value error and rounding (especially when order must be ASC or DESC). I.e. working on two files with 10-12 components (magnetic sextets) could cost approximetely 3 or 4 hours.
