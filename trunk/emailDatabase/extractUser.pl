 #!/usr/local/bin/perl -w

use strict;
use Net::SMTP;

my $delay_seconds = 15;

print "Enter the names of the persons...\n";
my @names;
my $line = <STDIN>;
while ($line ne ""){
  chomp($line);
  push(@names, $line);
  $line = <STDIN>;
  chomp($line);
}

print "finished\n";
print join(",", @names);


#print "Enter outputfile...\n";
#chomp(my $output = <STDIN>);
#open (OUT, ">$output");

print "START - " . `date`;

foreach my $user (<./maildir/*>){
	foreach my $folder (<$user/*>){
		foreach my $email (<$folder/*>){		
			my %email;
			$email{from} = "";
			my $prev_line = "";
			my $to_flag = "n";				
			my @to;
			my $target_flag = "n";
			open (IN, "<$email");
			while(<IN>){
				my $line = $_;
				$email{message} .= $line;	
				chomp($line);


				if ($line =~ /^Subject\:/){
					$email{subject} = $';
				}elsif ($line =~ /^From\:/){
					$email{from} = $';
				}elsif ($line =~ /To\:/){
					chomp(my $to_line = $');					
					$to_flag = 'y';
					if ($to_line =~ /\>,/)	{ push(@to, split(">, ", $to_line )); }
					elsif ($to_line =~ /;/)	{ push(@to, split("; ", $to_line)); }
					elsif($to_line =~ /,/)	{ push(@to, split(", ", $to_line));	}
					else 					{ push(@to, $to_line); }					
				}elsif($to_flag eq 'y'){
					if ($line =~/^\t/){
						chomp(my $to_line = $');
						if ($line =~ /\>,/){
							my @to2 = split(">, ", $to_line);
							push(@to, (split(">, ", $to_line)));
						}elsif ($line =~ /;/){
							my @to2 = split("; ", $line);
							push(@to, (split("; ", $to_line)));
						}elsif($line =~ /,/){
							my @to2 = split(", ", $line);
							push(@to, (split(", ", $to_line)));
						}else {
							push(@to, $to_line);
						}						
					}elsif(!($line =~ /^\t/)){
						$to_flag = 'n';
					}
				}
				$prev_line = $line;
			}
			$email{to} = @to;			
			close IN;			

			foreach my $email (@names){
				my $target_email = $email;
			  	foreach my $name (@to){
			    	if ($name =~ /$target_email/i){ $target_flag = 'y'; }
			  	}
			
				if($target_flag eq 'y') {		
				  	my $skip_flag = 'n';
				    my $smtp = Net::SMTP->new('smtp.psu.edu') or $skip_flag = 'y';
				    if($skip_flag eq 'y'){
				      print "email was skipped\n";
				    } elsif($skip_flag eq 'n'){
				      $smtp->mail("$email{from}");
				      $smtp->to('emailJunkiesTest2@gmail.com', 'emailJunkiesTest3@gmail.com');
				      $smtp->data();
				      $smtp->datasend("To: " . join(", ", @to) . "\n");
				      $smtp->datasend("Subject: $email{subject}");
				      $smtp->datasend("\n");
				      $smtp->datasend("$email{message}\n");
				      $smtp->dataend();
				      $smtp->quit();
				      sleep($delay_seconds);
				    }
				}
				$target_flag = "n";
			}
		}
	}
}

print "END - " . `date`;
