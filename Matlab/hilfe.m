[x11,y11] = importAgilentBin('scope_1.bin',1);
[x12,y12] = importAgilentBin('scope_1.bin',2);
%[x21,y21] = importAgilentBin('scope_5.bin',1);
%[x22,y22] = importAgilentBin('scope_5.bin',2);

%figure()
%plot(x11, y11, x12, y12)


%for i = 2:1:length(y11)-1
%    if y11(i) ~= y11(i-1) && y11(i) ~= y11(i+1)
%        y11(i) = y11(i-1);
%    end
%end

%for i = 2:1:length(y12)-1
%    if y12(i) ~= y12(i-1) && y12(i) ~= y12(i+1)
%        y12(i) = y12(i-1);
%    end
%end


for i = 1:1:length(y11)
    if y11(i) > 3
        y11(i) = 6;
    else
        y11(i) = 0;
    end
end

for i = 1:1:length(y12)
    if y12(i) > 2.4
        y12(i) = 5.5;
    else
        y12(i) = 0.5;
    end
end

figure()
plot(x11, y11, x12, y12);
xlim([0,0.001])

counter = 0;
ySectorCounter = [];

if y11(1) == 0
    for i = 1:1:length(y11)-5
        counter = counter + 1;
        if y11(i) == 6 && y11(i+1) == 0 && y11(i+2) == 0 && y11(i+3) == 0 && y11(i+4) == 0 && y11(i+5) == 0
            ySectorCounter(length(ySectorCounter)+1) = counter;
            counter = 0;
        end
    end
else
    for i = 1:1:length(y11)-5
        counter = counter + 1;
        if y11(i) == 0 && y11(i+1) == 6 && y11(i+2) == 6 && y11(i+3) == 6 && y11(i+4) == 6 && y11(i+5) == 6
            ySectorCounter(length(ySectorCounter)+1) = counter;
            counter = 0;
        end
    end        
end

highLevel = 0;
lowLevel = 0;
arraySector = 0;
oldArraySector = 1;

for tab = ySectorCounter
    for i = oldArraySector:1:(tab + arraySector)
        if y12(i) == 5.5
            highLevel = highLevel + 1;
        else
            lowLevel = lowLevel + 1;
        end
    end
    if highLevel > lowLevel
        y12(oldArraySector:tab + arraySector) = 5.5;
    else
        y12(oldArraySector:tab + arraySector) = 0.5;
    end
    highLevel = 0;
    lowLevel = 0;
    oldArraySector = oldArraySector + tab;
    arraySector = arraySector + tab;
end

figure()
plot(x11, y11, x12, y12)
xlim([0,0.001])
%ylim([-1,6])
%xlim([0, 0.01])
%figure()
%plot(x11,y11,x12,y12,x21,y21+6,x22,y22+6)
%figure()
%plot(x21,y21+6,x22,y22+6)
%ylim([-1,6])
