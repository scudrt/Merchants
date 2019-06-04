using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract{
    //trading parties
    private Company _offerer, _target;
    public Company sourceCompany { set; get; }
    public Company targetCompany { set; get; }

    /****************trading content****************/
    private List<Talent> _offeredTalentList, _requiredTalentList;
    private List<Block> _offeredBlockList, _requiredBlockList;
    private float _offeredFund, _requiredFund;
    /****************other private arguments****************/
    private bool _isTargetAgreed;
    private bool _isContractConfirmed;

    private Contract() { }
    public Contract(Company offerer, Company target = null) {
        _offeredFund = 0f;
        _requiredFund = 0f;
        
        _isTargetAgreed = false;
        _isContractConfirmed = false;

        _offerer = offerer;
        _target = target;

        _offeredTalentList = new List<Talent>();
        _requiredTalentList = new List<Talent>();
        _offeredBlockList = new List<Block>();
        _requiredBlockList = new List<Block>();
    }
    /****************private functions****************/
    private bool waitForTargetResponse() {
        //return true if the target company responded this contract
        //_isTargetAgreed = something
        return true;
    }

    private bool deal() {
        //trade the resource, remember to notify target
        //return false if one of the parties doesn't have enough resource
        if (_isTargetAgreed) {
            //swap resource
            ;
            return true;
        } else {
            return false;
        }
    }

    /****************public functions****************/
    public bool confirm() {
        //send the contract request to the target and solve the
        //return false if confirming failed
        if (_target == null) {
            return false;
        }
        _isContractConfirmed = true;
        waitForTargetResponse();
        return deal();
    }

    public void setTarget(Company target) {
        _target = target;
    }

    public void setOfferedFund(float fund) {
        //negative number means require money
        _offeredFund = fund;
    }

    public void changeOfferedFundBy(float deltaFund) {
        _offeredFund += deltaFund;
    }

    public void addTalent(Talent talent) {
        if (talent.companyBelong == _offerer) {
            _offeredTalentList.Add(talent);
        }else if (talent.companyBelong == _target) {
            _requiredTalentList.Add(talent);
        } else {
            //wrong talent, error
            Debug.Log("Contract: wrong talent input.");
        }
    }

    public void addBlock(Block block) {
        if (block.companyBelong == _offerer) {
            _offeredBlockList.Add(block);
        } else if (block.companyBelong == _target) {
            _requiredBlockList.Add(block);
        } else {
            //wrong block, error
            Debug.Log("Contract: wrong block input.");
        }
    }

    public void removeTalent() {
        ;
    }

    public void removeBlock() {
        ;
    }
}
